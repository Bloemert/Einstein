using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Lib.Config.Extensions
{
	public static class EncryptedConfigProviderExtensions
	{
		public static IConfigurationBuilder AddEncryptedProvider(this IConfigurationBuilder builder)
		{
			return builder.Add(new EncryptedConfigProvider());
		}
	}
}
