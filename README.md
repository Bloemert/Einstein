# README #

Einstein - Tech Radar
Free and opensource Tech Radar for companies or communities who want to track there skills and knowledge!

Version: 0.1 Under development!

### How do I get set up? ###

Summary of set up, Configuration, Dependencies:

	* REST Layer:
		** Overall:   
			*** C# Dotnet core v2.+ https://dotnet.github.io/ 
			*** Dependency Injection using AutoFac https://autofac.org/ (Via NuGet)
			*** Mapping objects between layers using AutoMapper: https://automapper.org/ (Via NuGet)
			*** Logging using Serilog https://serilog.net/  (Via NuGet)
		** Database: 	
			*** Microsoft SQL Server 2014+	 https://www.microsoft.com/nl-nl/sql-server/sql-server-2017
			*** Microsoft SQL Server Data Tools (SSDT) https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017
		** DataLayer:	
			*** Martin Fowler's Repository pattern to "hide" the SQL for the rest of the app.
		** ConnectionLayer:
			*** NancyFX Web Framework for .NET http://nancyfx.org/
			*** Microsoft Kestrel Webserver https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1&tabs=aspnetcore2x
	* UI Layer:
		** ASP.NET Core 2.0 https://github.com/aspnet/home
		** Vui.js 2.0 https://vuejs.org/
		** Buefy https://buefy.github.io/#/
		** Font Awesome 5+ https://fontawesome.com/
		** and more ...
		

### Contribution guidelines ###

* Help wanted!

### Who do I talk to? ###

* For bugs, feature requests, and/or idea's use the Einstein issue tracker on GitHub: https://github.com/Bloemert/Einstein/issues
