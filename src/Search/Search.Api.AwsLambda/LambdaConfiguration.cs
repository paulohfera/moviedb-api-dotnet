using Microsoft.Extensions.Configuration;
using System.IO;

namespace Search.Api.AwsLambda
{
	public class LambdaConfiguration : ILambdaConfiguration
	{
		public static IConfiguration Configuration => new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

		IConfiguration ILambdaConfiguration.Configuration => Configuration;
	}
}