using Microsoft.Extensions.Configuration;

namespace Search.Api.AwsLambda
{
	public interface ILambdaConfiguration
	{
		IConfiguration Configuration { get; }
	}
}
