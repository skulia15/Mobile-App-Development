using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Genres;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace MovieSearch.Services
{
	public class MovieConverter : IMovieConverter
	{
		private ImageDownloader imageDownloader = new ImageDownloader(new StorageClient());
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
			string localPathForImage = await getPoster(movie);

			var creditResponse = await movieApi.GetCreditsAsync(movie.Id);
			var movieResponse = await movieApi.FindByIdAsync(movie.Id);
			List<string> cast = GetCast(creditResponse);
			List<string> genres = GetGenres(movie);

			return new Movie
			{
				Title = movie.Title,
				Year = movie.ReleaseDate.Year,
				Cast = cast,
				ImageName = localPathForImage,
				Genres = genres,
				Description = movie.Overview,
				Runtime = movieResponse.Item.Runtime
			};
		}

		private async Task<string> getPoster(MovieInfo movie)
		{
			string localPathForImage = imageDownloader.LocalPathForFilename(movie.PosterPath);
			if (File.Exists(localPathForImage) == false && localPathForImage != "")
			{
				await imageDownloader.DownloadImage(movie.PosterPath, localPathForImage, CancellationToken.None);

			}

			return localPathForImage;
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