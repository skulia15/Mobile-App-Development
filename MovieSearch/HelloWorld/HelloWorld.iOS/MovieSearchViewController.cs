using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS
{
    public class MovieSearchViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

        public MovieSearchViewController(IMovieDbSettings settings)
        {
            MovieDbFactory.RegisterSettings(settings);
        }

        public int HttpGet { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;

            var promptLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width-2*StartX, Height),
                Text = "Enter words in movie title: "
            };

            this.View.AddSubview(promptLabel);

            var searchField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY+Height, this.View.Bounds.Width-2*StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            this.View.AddSubview(searchField);

            var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchButton.Frame = new CGRect(StartX, StartY+2*Height, this.View.Bounds.Width-2*StartX, Height);
            searchButton.SetTitle("Get Movie", UIControlState.Normal);

            this.View.AddSubview(searchButton);

            var movieLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY+3*Height, this.View.Bounds.Width-2*StartX, Height),
                Text = ""
            };
            this.View.AddSubview(movieLabel);

            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value; 

            searchButton.TouchUpInside += async (sender, args) =>
            {
                searchField.ResignFirstResponder();
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(searchField.Text);
                movieLabel.Text = response.Results[0].Title;
            };
        }
    }
}