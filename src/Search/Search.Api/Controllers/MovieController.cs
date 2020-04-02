using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Search.Api.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Search.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MovieController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public MovieController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		[Route("Search/{title}/{page}")]
		public async Task<SearchResult> Search(string title, int page)
		{
			var omdbKey = _configuration.GetValue<string>("OmdbKey");
			var omdbUrl = _configuration.GetValue<string>("OmdbUrl");
			var searchApi = $"{omdbUrl}{omdbKey}&s={title}&page={page}";

			var json = await new HttpClient().GetStringAsync(searchApi);
			var result = JsonSerializer.Deserialize<SearchResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
