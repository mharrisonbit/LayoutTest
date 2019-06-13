using MvvmHelpers;
using System.Collections.ObjectModel;
using LayoutTestApp.Models;
using Xamarin.Forms;
using System;
using LayoutTestApp.Helpers;
using System.Threading.Tasks;

namespace LayoutTestApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {

        public HomePageViewModel()
        {
            CollectionItems = new ObservableCollection<Movie>();
            PopulatePostersAsync();
        }

        ObservableCollection<Movie> _CollectionItems;
        public ObservableCollection<Movie> CollectionItems
        {
            get { return _CollectionItems; }
            set { SetProperty(ref _CollectionItems, value); }
        }

        //ObservableCollection<Movie> _CollectionItems;
        //public ObservableCollection<Movie> CollectionItems
        //{
        //    get { return _CollectionItems; }
        //    set { SetProperty(ref _CollectionItems, value); }
        //}

        Movie _SelectedPoster;
        public Movie SelectedPoster
        {
            get { return _SelectedPoster; }
            set 
            {
                if (_SelectedPoster != value)
                {
                    SetProperty(ref _SelectedPoster, value);
                }
            }
        }

        Uri _TestImage;
        public Uri TestImage
        {
            get { return _TestImage; }
            set { SetProperty(ref _TestImage, value); }
        }

        private async Task PopulatePostersAsync()
        {
            var address = Constants.ApiUrl + Constants.Movies;
            //CollectionItems = new ObservableCollection<Movie>();
            var listOfItems = new ObservableCollection<Movie>();

            var answer = await new ApiCalls().GetDataFromServer(address, "GET");
            try
            {
                var i =  0;
                foreach (var item in answer["data"])
                {
                    Uri posterUri = new Uri(Constants.ApiUrl + item["field_movie_poster"]);
                    Uri websiteUrl = new Uri(item["field_movie_website_url"].ToString());

                    listOfItems.Add(new Movie
                    {
                        Name = item["title"].ToString(),
                        Id = i + 1,
                        PosterLink = posterUri,
                        Description = item["field_movie_description"].ToString(),
                        Rating = item["field_movie_rating"].ToString(),
                        Uri = websiteUrl
                    });
                }
                //CollectionItems = listOfItems;
            }
            catch (Exception ex)
            {
                CollectionItems = null;
                Console.WriteLine("ERROR!!!                   HomePageViewModel:PopulatePostersAsync " + ex.Message);
            }
        }


        public Command RefreshCmdBtn => new Command(() =>
        {
            PopulatePostersAsync();
        });
    }
}