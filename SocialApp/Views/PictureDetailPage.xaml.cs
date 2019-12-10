using SocialApp.Services;
using SocialApp.ModelViews;
using Xamarin.Forms;
using System;

using Plugin.Media;
using Plugin.Media.Abstractions;

namespace SocialApp.Views
{
    public partial class PicturesDetailPage : ContentPage
    {
        public PicturesDetailPage(PicturesViewModel viewModel)
        {

            InitializeComponent();

            var pictureStore = new SQLitePicturePosts(DependencyService.Get<ISQLiteDB>());
            var pageService = new PageService();
            Title = (viewModel.Phone == null) ? "New Picture" : "Edit Picture";
            BindingContext = new PictureDetailViewModel(viewModel ?? new PicturesViewModel(), pictureStore, pageService);

        }
        void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    App.Current.MainPage.DisplayAlert("Notification1", "Successfully Login", "Okay");
                    break;
                case SwipeDirection.Right:
                    App.Current.MainPage.DisplayAlert("Notification2", "Successfully Login", "Okay");
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    break;
            }
        }
    }
}