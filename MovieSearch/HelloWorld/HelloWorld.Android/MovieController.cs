using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieDownload;
using MovieSearch.Services;
using System.Threading.Tasks;

namespace MovieSearch.Droid
{
	public class MovieController
	{
		IMovieConverter converter;
		public MovieController()
		{			
			converter = new MovieConverter(new MovieDbSettings());
		}

		public async Task<Movie> GetSingleMovieAsync(string searchTerm)
		{
			List<Movie> movies = await converter.GetMoviesByTitleAsync(searchTerm);
			return movies.FirstOrDefault();
		}
	}
}