
using SocialApp.Services;
using SocialApp.ModelViews;
using Xamarin.Forms;

namespace SocialApp.Views
{
    public partial class PicturesPage : ContentPage
    {
        public PicturesPage()
        {
            var pictureStore = new SQLitePicturePosts(DependencyService.Get<ISQLiteDB>());
            var pageService = new PageService();

            ViewModel = new PicturesPageViewModel(pictureStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnPictureSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectPictureCommand.Execute(e.SelectedItem);
        }

        public PicturesPageViewModel ViewModel
        {
            get { return BindingContext as PicturesPageViewModel; }
            set { BindingContext = value; }
        }
    }
}