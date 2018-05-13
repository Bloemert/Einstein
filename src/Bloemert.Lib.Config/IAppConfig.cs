using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Lib.Config
{
	public interface IAppConfig
	{
		string ApplicationName { get; }

		IConfiguration BaseConfiguration { get; }

		string ConnectionString { get; }

		string GetValue(string key);

		IDictionary<string, string> GetValues(string key);

	}
}
