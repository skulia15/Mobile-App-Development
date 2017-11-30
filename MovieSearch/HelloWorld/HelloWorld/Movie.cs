using DM.MovieApi.MovieDb.Genres;
using System;
using System.Collections.Generic;

namespace MovieSearch
{
	public class Movie
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public List<string> Cast { get; set; }
		public string ImageName { get; set; }
		public List<string> Genres { get; set; }
		public int Runtime { get; set; }
		public string Description { get; set; }
	}
}