using Einstein.WebAPI.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace Einstein.WebAPI.Modules
{
	public class InfoModule : NancyModule
	{
		public InfoModule()
			: base("/info")
		{
			Get("/", args => View["index",
				new Info
				{
					Title = "Einstein RestAPI - Application information",
					NancySection = GetNancysInfo(),
					DotNetSection = GetDotNetInfo(),
					AssembliesSection = GetAssembliesInfo()
					}]);

		}


		public InfoSection GetNancysInfo()
		{
			var result = new InfoSection
			{
				Title = "Nancy's detail info",
				Description = "C# Web framework alla Sintra for Ruby",
				Link = "https://github.com/NancyFx/Nancy"
			};

			result.Items = new List<InfoItem>();

			result.Items.Add(new InfoItem{ Name = "ControlPanelEnabled", Value = Context.ControlPanelEnabled.ToString() });
			result.Items.Add(new InfoItem{ Name = "Culture", Value = Context.Culture.DisplayName });
			result.Items.Add(new InfoItem{ Name = "CurrentUser", Value = (Context.CurrentUser!=null ? Context.CurrentUser.ToString() : "-")});
			result.Items.Add(new InfoItem{ Name = "Environment", Value = Context.Environment!=null ? Context.Environment.ToString() : "-"});
			result.Items.Add(new InfoItem{ Name = "ResolvedRoute", Value = Context.ResolvedRoute!=null ? Context.ResolvedRoute.Description.Name : "-" });
			result.Items.Add(new InfoItem{ Name = "RootPath", Value = Response.RootPath });

			return result;
		}


		public InfoSection GetAssembliesInfo()
		{
			var result = new InfoSection
			{
				Title = "Assemblies info",
				Description = "Assembly - .NET Runtime module class",
				Link = "https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly?view=netcore-2.1"
			};

			result.Items = new List<InfoItem>();

			Assembly entryAsm = Assembly.GetEntryAssembly();
			AssemblyName entryAsmName = entryAsm.GetName();

			Assembly exeAsm = Assembly.GetExecutingAssembly();
			AssemblyName exeAsmName = exeAsm.GetName();

			Assembly callingAsm = Assembly.GetCallingAssembly();
			AssemblyName callingAsmName = callingAsm.GetName();

			result.Items.Add(new InfoItem { Name = "Entry Assembly Name", Value = entryAsmName.FullName });
			result.Items.Add(new InfoItem { Name = "Entry Assembly Version", Value = String.Format("Version: {0}", entryAsmName.Version) });
			result.Items.Add(new InfoItem { Name = "Entry Assembly Codebase", Value = entryAsmName.CodeBase });

			result.Items.Add(new InfoItem { Name = "Executing Assembly", Value = exeAsmName.FullName });
			result.Items.Add(new InfoItem { Name = "Executing Assembly Version", Value = String.Format("Version: {0}", exeAsmName.Version) });
			result.Items.Add(new InfoItem { Name = "Executing Assembly Codebase", Value = exeAsmName.CodeBase });

			result.Items.Add(new InfoItem { Name = "Calling Assembly", Value = callingAsmName.FullName });
			result.Items.Add(new InfoItem { Name = "Calling Assembly Version", Value = String.Format("Version: {0}", callingAsmName.Version) });
			result.Items.Add(new InfoItem { Name = "Calling Assembly Codebase", Value = callingAsmName.CodeBase });

			return result;
		}

		public InfoSection GetDotNetInfo()
		{
			var result = new InfoSection
			{
				Title = ".NET info",
				Description = ".NET versions",
				Link = "https://docs.microsoft.com/en-us/dotnet/standard/frameworks"
			};

			result.Items = new List<InfoItem>();


			result.Items.Add(new InfoItem { Name = "Host platform", Value = System.Runtime.InteropServices.RuntimeInformation.OSDescription, Description = "" });

			var framework = Assembly
									.GetEntryAssembly()?
									.GetCustomAttribute<TargetFrameworkAttribute>()?
									.FrameworkName;
			result.Items.Add(new InfoItem { Name = "Version", Value = framework, Description = "" });

			string tfm = "Unknown";
#if NETCOREAPP1_0
			tfm = "NETCOREAPP1_0";
#elif NETCOREAPP1_1
			tfm = "NETCOREAPP1_1";
#elif NETCOREAPP2_0
			tfm = "NETCOREAPP2_0";
#elif NETCOREAPP2_1
			tfm = "NETCOREAPP2_1";
#endif
			result.Items.Add(new InfoItem { Name = "TFM", Value = tfm, Description = "TFM: Target Framework Moniker" });


			
			result.Items.Add(new InfoItem { Name = "RuntimeDirectory", Value = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), Description = "" });



			return result;
		}
	}
}
