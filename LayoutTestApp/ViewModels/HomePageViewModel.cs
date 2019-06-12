using MvvmHelpers;
using System.Collections.ObjectModel;
using LayoutTestApp.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace LayoutTestApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            PopulatePosters();
        }

        ObservableCollection<MoviePoster> _CollectionItems;
        public ObservableCollection<MoviePoster> CollectionItems
        {
            get { return _CollectionItems; }
            set { SetProperty(ref _CollectionItems, value); }
        }

        MoviePoster _SelectedPoster;
        public MoviePoster SelectedPoster
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

        private void PopulatePosters()
        {
            var listOfItems = new ObservableCollection<MoviePoster>();
            for (int i = 0; i < 10; i++)
            {
                listOfItems.Add(new MoviePoster
                {
                    Name = "Runner.jpg",
                    Id = i
                });
            }
            _CollectionItems = listOfItems;
        }


        public Command RefreshCmdBtn => new Command(() =>
        {
            PopulatePosters();
        });
    }
}