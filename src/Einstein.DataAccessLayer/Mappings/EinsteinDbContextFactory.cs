using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Einstein.DataAccessLayer.Mappings
{
  public class EinsteinDbContextFactory : IDesignTimeDbContextFactory<EinsteinDbContext>
  {
	 public EinsteinDbContext CreateDbContext(string[] args)
	 {
		// Get appsettings.json config info
		string envName = Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName") ?? "Develop";
		string userEnvConfigFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Einstein.WebAPI\\appsettings.{envName}.json";

		if (File.Exists(userEnvConfigFile))
		{
		  Console.Out.WriteLine("Found: {0}", userEnvConfigFile);
		}
		else
		{
		  Console.Out.WriteLine("NOT found: {0}", userEnvConfigFile);
		}

		  // All config files must be added in THIS spot!
		  var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true)
			.AddJsonFile(userEnvConfigFile, optional: true)
			.AddJsonFile("logsettings.json", optional: true, reloadOnChange: true)
			.Build();

		var connectionString = config.GetConnectionString("DefaultDB");


		var optionsBuilder = new DbContextOptionsBuilder<EinsteinDbContext>();
		optionsBuilder
			.UseSqlServer(connectionString, x => x.MigrationsAssembly("Einstein.DataAccessLayer"));

		return new EinsteinDbContext(optionsBuilder.Options);
	 }
  }
}
