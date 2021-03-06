﻿using System.Collections.Generic;

namespace Search.Api.Models
{
	public class SearchResult
	{
		public IEnumerable<TitleResult> Search { get; set; }
		public string TotalResults { get; set; }
		public string Response { get; set; }
	}
}
