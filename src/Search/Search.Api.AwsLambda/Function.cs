using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Search.Api.AwsLambda.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace Search.Api.AwsLambda
{
	public class Function
	{
		private ILambdaConfiguration Configuration { get; }

		public Function()
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			var serviceProvider = serviceCollection.BuildServiceProvider();
			Configuration = serviceProvider.GetService<ILambdaConfiguration>();
		}

		private void ConfigureServices(IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<ILambdaConfiguration, LambdaConfiguration>();
		}

		public async Task<SearchResult> FunctionHandler(Filter filter, ILambdaContext context)
		{
			var omdbKey = LambdaConfiguration.Configuration["OmdbKey"];
			var omdbUrl = LambdaConfiguration.Configuration["OmdbUrl"];
			var searchApi = $"{omdbUrl}{omdbKey}&s={filter.Title}&page={filter.Page}";

			var json = await new HttpClient().GetStringAsync(searchApi);
			var result = JsonSerializer.Deserialize<SearchResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
