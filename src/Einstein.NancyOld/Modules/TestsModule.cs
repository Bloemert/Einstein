using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Nancy;
using Nancy.Responses;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bloemert.WebAPI.Auth.Modules
{
	public class TestsModule : NancyModule
	{

		public TestsModule()
			: base("/tests")
		{
			Get("/stream/LineCounts.txt", args =>
			{
				return new StreamResponse(() => new FileStream(@"D:\temp\LineCounts.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "text/plain");
			});


			Get("/stream/Tiny.txt", args =>
			{
				return new StreamResponse(() => new FileStream(@"D:\temp\Tiny.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "text/plain");
			});

			Get("/stream/Big.txt", args =>
			{
				return new StreamResponse(() => new FileStream(@"D:\temp\Big.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "text/plain");
			});

			Get("/stream/BigAttached.txt", args =>
			{
				return new StreamResponse(() => new FileStream(@"D:\temp\Big.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "text/plain")
						.AsAttachment("BigStreamAttached.txt", "text/plain");
			});




			Get("/stream/Big.zip", args =>
			{
				return new StreamResponse( () => new FileStream(@"D:\temp\1GB.zip", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "application/zip");
			});

			Get("/stream/BigAttached.zip", args =>
			{
				return new StreamResponse(() => new FileStream(@"D:\temp\1GB.zip", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "application/zip")
						.AsAttachment("BigAttached.zip", "application/zip");
			});




			Get("/faster/Big.zip", args =>
			{
				return new GenericFileResponse(@"d:\temp\1GB.zip", this.Context);
			});

			Get("/faster/BigAttached.zip", args =>
			{
				return new GenericFileResponse(@"/1GB.zip", this.Context)
						.AsAttachment("BigAttached.zip", "application/zip");
			});
		}
	}
}
