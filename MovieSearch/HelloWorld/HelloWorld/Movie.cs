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

		public string getStringedGenres()
		{
			string stringedGenres = "";
			foreach (string g in Genres)
			{
				if (stringedGenres != "")
				{
					stringedGenres += ", ";
				}
				stringedGenres += g;
			}
			return stringedGenres;
		}

		public string CastToString() // pun intended
		{
			string castMembers = "";
			for (int i = 0; i < Cast.Count && i < 3; i++)
			{
				if (i != 0)
				{
					castMembers += ", ";
				}
				castMembers += Cast[i];
			}
			return castMembers;
		}
	}
}