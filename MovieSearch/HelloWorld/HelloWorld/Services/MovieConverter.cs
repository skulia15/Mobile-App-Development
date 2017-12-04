using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Genres;
using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieSearch.Services
{
	public class MovieConverter : IMovieConverter
	{
		IApiMovieRequest movieApi;

		public MovieConverter(IMovieDbSettings settings)
		{
			MovieDbFactory.RegisterSettings(settings);
			movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
		}

		public async Task<List<Movie>> GetMoviesByTitleAsync(string title)
		{
			List<Movie> movies = new List<Movie>();

			if (title != null && title != "")
			{
				var titleResponse = await movieApi.SearchByTitleAsync(title);
				movies = await GetMoviesFromResponse(titleResponse);
			}
			return movies;
		}

		private async Task<List<Movie>> GetMoviesFromResponse(ApiSearchResponse<MovieInfo> response)
		{
			List<Movie> movies = new List<Movie>();
			foreach (MovieInfo m in response.Results)
			{
				var movie = await GetMovieFromMovieInfo(m);
				movies.Add(movie);
			}
			return movies; 
		}

		public async Task<List<Movie>> GetTopRatedMoviesAsync()
		{
			var topRated = await movieApi.GetTopRatedAsync();
			var movies = await GetMoviesFromResponse(topRated);
			return movies;
		}

		public async Task<Movie> GetMovieFromMovieInfo(MovieInfo movie)
		{
			var creditResponse = await movieApi.GetCreditsAsync(movie.Id);
			var movieResponse = await movieApi.FindByIdAsync(movie.Id);
			List<string> cast = GetCast(creditResponse);
			List<string> genres = GetGenres(movie);

			return new Movie
			{
				Title = movie.Title,
				Year = movie.ReleaseDate.Year,
				Cast = cast,
				ImageName = movie.PosterPath,
				Genres = genres,
				Description = movie.Overview,
				Runtime = movieResponse.Item.Runtime
			};
		}

		private static List<string> GetGenres(MovieInfo m)
		{
			List<string> genres = new List<string>();
			foreach (Genre g in m.Genres)
			{
				genres.Add(g.Name);
			};
			return genres;
		}

		private static List<string> GetCast(ApiQueryResponse<MovieCredit> creditResponse)
		{
			List<string> cast = new List<string>();
			foreach (MovieCastMember c in creditResponse.Item.CastMembers)
			{
				cast.Add(c.Name);
			}

			return cast;
		}
	}
}