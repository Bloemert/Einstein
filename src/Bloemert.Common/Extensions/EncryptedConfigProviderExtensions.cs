using Bloemert.Common.Config;
using Microsoft.Extensions.Configuration;

namespace Bloemert.Common.Extensions
{
	public static class EncryptedConfigProviderExtensions
	{
		public static IConfigurationBuilder AddEncryptedProvider(this IConfigurationBuilder builder)
		{
			return builder.Add(new EncryptedConfigProvider());
		}
	}
}
