using Autofac;
using Autofac.Extensions.DependencyInjection;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Einstein.WebAPI.Auth;
using Einstein.WebAPI.Extensions;
using Einstein.WebAPI.Types;
using GraphQL.Authorization;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Einstein.WebAPI
{
  public class Startup
  {
	 public Startup(IWebHostEnvironment env)
	 {
		var builder = new ConfigurationBuilder()
												.SetBasePath(env.ContentRootPath)
												.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
												.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
												.AddEnvironmentVariables();
		this.Configuration = builder.Build();
	 }

	 public IConfiguration Configuration { get; }

	 public ILifetimeScope IoC { get; set; }

	 // This method gets called by the runtime. Use this method to add services to the container.
	 public IServiceProvider ConfigureServices(IServiceCollection services)
	 {

		services.AddControllers();

		services.AddIdentityServer(/*options =>
		{
		  options.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions
		  {
			 LoginUrl = "/login.html"
		  };
		}*/)
			.AddDeveloperSigningCredential()
			.AddInMemoryIdentityResources(new List<IdentityResource>
			 {
					new IdentityResources.OpenId(),
					new IdentityResources.Email(),
					new IdentityResources.Profile(),
					new IdentityResources.Phone(),
					new IdentityResources.Address(),
					new IdentityResource{
						Name = "role", DisplayName = "Role", UserClaims = new List<string> {ClaimTypes.Role }
					}
			 })
			.AddInMemoryApiResources(new List<ApiResource>
			{
			  new ApiResource
		  {
				Name = "postmantestclient",

            // secret for using introspection endpoint
            ApiSecrets =
				{
					 new Secret("PostmanSecret".Sha256())
				},

            // include the following using claims in access token (in addition to subject id)
            UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email, JwtClaimTypes.Role},

            // this API defines two scopes
            Scopes =
						{
							 new Scope()
							 {
									Name = "postmantestclient.full_access",
									DisplayName = "Full access to postmantestclient"
							 },
							 new Scope
							 {
									Name = "postmantestclient.read_only",
									DisplayName = "Read only access to postmantestclient"
							 }
						}
				}
			})
			.AddInMemoryClients(new List<Client>{
				new Client
				{
					 ClientId = "postmantestclient",
					 ClientName = "Postman http test client",
					 AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
					 AllowedScopes = new List<string>{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							"postmantestclient.full_access"
					 },
					 IdentityTokenLifetime = 60 * 60 * 24,
					 AccessTokenLifetime = 60 * 60 * 24,
					 RequireConsent = false,
					 RequirePkce = false,
					 RequireClientSecret = false,
					 ClientSecrets = new List<Secret>
					 {
							new Secret("PostmanSecret".Sha256())
					 },

					 RedirectUris = new List<string>()
					 {
							"https://www.getpostman.com/oauth2/callback"
					 }
				 }
			})
			.AddTestUsers(new List<IdentityServer4.Test.TestUser>
			{
				new IdentityServer4.Test.TestUser{
					IsActive = true,
					SubjectId = "1",
					Username = "admin",
					Password = "admin!",
					Claims = new List<Claim>{
						new Claim("role", "Admin"),
					}
				}
			});

		services.AddAuthentication(options =>
		{
		  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		  options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
		  options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
		  options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
		  options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
		  options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
		})
		.AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
		{
		  options.Authority = "https://localhost:44328";
		});


		//services.AddAuthorization(options =>
		//{
		//options.AddPolicy("AdminPolicy", policy =>
		//{
		//	 //policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
		//	 policy.RequireAuthenticatedUser();
		//});
		//});


		services.AddMvc(options => options.EnableEndpointRouting = false);


		services.AddCors();

		services.Configure<IISServerOptions>(options =>
		{
		  options.AllowSynchronousIO = true;
		});

		services.AddHttpContextAccessor();

		services.AddGraphQL(_ =>
		{
		  _.ExposeExceptions = true;
		})
		.AddUserContextBuilder<GraphQLUserContextBuilder>()
		.AddDataLoader();

		services.AddGraphQLAuth();


		// Initialize Autofac IoC
		var builder = new ContainerBuilder();

		ConfigureContainer(builder);

		// When you do service population, it will include your controller
		// types automatically.
		builder.Populate(services);

		this.IoC = builder.Build();
		return new AutofacServiceProvider(this.IoC);
	 }


	 public void ConfigureContainer(ContainerBuilder builder)
	 {
		builder.RegisterModule(new Bloemert.Common.ModuleLoader { ApplicationName = "Einstein.WebAPI" });

		builder.RegisterModule<Einstein.DataAccessLayer.ModuleLoader>();

		builder.RegisterType<DefaultUserIdentityProvider>()
		.As<IUserIdentityProvider>()
		.InstancePerLifetimeScope();

		EinsteinSchema.RegisterGraphQLTypes(builder);
	 }

	 // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	 {

		app.UseIdentityServer();

		app.UseCors();

		app.UseRouting();

		app.UseStaticFiles();

		if (env.IsDevelopment())
		{
		  app.UseDeveloperExceptionPage();
		}
		else
		{
		  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		  app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseMvc();

		// add http for Schema at default url api/graphql
		app.UseGraphQL<ISchema>("/api/graphql");

		app.UseGraphQLWithAuth();

		app.UseEndpoints(endpoints =>
		{
		  endpoints.MapControllers();
		});

		// use graphql-playground at default url /ui/playground
		app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { Path = "/ui/graphql", GraphQLEndPoint = "/api/graphql" });
	 }

  }
}
