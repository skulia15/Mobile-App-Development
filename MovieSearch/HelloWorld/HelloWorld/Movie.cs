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
	}
}