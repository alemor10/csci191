using SocialApp.Services;
using SocialApp.ModelViews;
using Xamarin.Forms;
using System;

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



    }
}