using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Services
{
	public interface IMovieConverter
	{
		Task<List<Movie>> GetMoviesByTitleAsync(string title);
		Task<List<Movie>> GetTopRatedMoviesAsync();
	}
}
