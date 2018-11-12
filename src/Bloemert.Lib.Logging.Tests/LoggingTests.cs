using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Bloemert.Lib.Logging.Tests
{
	public class LoggingTests
	{
		private IContainer IoC { get; set; }

		private string NewestLogFile { get; set; }

		public LoggingTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<Bloemert.Lib.Logging.ModuleLoader>();

			builder.RegisterType<MyDepTest>()
				.As<IMyDepTest>()
				.InstancePerDependency();

			IoC = builder.Build();
		}


		~LoggingTests()
		{
			// Opruimen werkt niet omdat Logger nog actief is
			File.SetAttributes(NewestLogFile, FileAttributes.Normal);
			File.Delete(NewestLogFile);
			Assert.False(File.Exists(NewestLogFile));
		}


		[Fact]
		public void TestByDependency()
		{
			IMyDepTest tst = IoC.Resolve<IMyDepTest>();
			tst = null;

			// Check logfiles (see logsettings.json!)
			string logDirPath = @"c:/temp/Logs/";
			DirectoryInfo logDir = new DirectoryInfo(logDirPath);
			IList<FileInfo> logFiles = logDir.GetFiles("RollingFileTest-*.json").ToList();
			FileInfo newestLogFile = logFiles.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

			Assert.True(File.Exists(newestLogFile.FullName));

			using (FileStream fileStream = new FileStream(newestLogFile.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				string logfileContent = new StreamReader(fileStream).ReadToEnd();

				Assert.Contains("\"SourceContext\":\"Bloemert.Lib.Logging.Tests.MyDepTest\",\"Application\":\"EinsteinTest\"", logfileContent);
			}


		}


	}
}
