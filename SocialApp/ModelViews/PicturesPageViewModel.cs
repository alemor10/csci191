
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SocialApp.Models;
using SocialApp.Views;
using SocialApp.Services;
using Xamarin.Forms;
namespace SocialApp.ModelViews
{
    public class PicturesPageViewModel:BaseViewModel
    {
        private PicturesViewModel _selectedPost;
        private IPicturePostStore _postStore;
        private IPageService _pageService;

        private bool _isDataLoaded;


        public ObservableCollection<PicturesViewModel> Posts { get; private set; }
        = new ObservableCollection<PicturesViewModel>();

        public PicturesViewModel SelectedPost
        {
            get { return _selectedPost; }
            set { SetValue(ref _selectedPost, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddPictureCommand { get; private set; }
        public ICommand SelectPictureCommand { get; private set; }
        public ICommand DeletePictureCommand { get; private set; }


        public PicturesPageViewModel(IPicturePostStore pictureStore, IPageService pageService)
        {
            _postStore = pictureStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddPictureCommand = new Command(async () => await AddPicture());
            SelectPictureCommand = new Command<PicturesViewModel>(async c => await SelectPicture(c));
            DeletePictureCommand = new Command<PicturesViewModel>(async c => await DeletePicture(c));

            MessagingCenter.Subscribe<PictureDetailViewModel, PicturePost>
                (this, Events.PostAdded, OnPictureAdded);

            MessagingCenter.Subscribe<PictureDetailViewModel, PicturePost>
            (this, Events.PictureUpdated, OnPictureUpdated);
        }



        private void OnPictureAdded(PictureDetailViewModel source, PicturePost post)
        {
            Posts.Add(new PicturesViewModel(post));
        }

        private void OnPictureUpdated(PictureDetailViewModel source, PicturePost post)
        {
            var postInList = Posts.Single(c => c.ID== post.ID);

            postInList.ID = post.ID;
            postInList.PictureTitle = post.PictureTitle;
            postInList.PictureCategory = post.PictureCategory;
            postInList.PictureRating = post.PictureRating;
            postInList.PicturePath = post.PicturePath;
            postInList.PictureLocation = post.PictureLocation;
            postInList.PictureTime = post.PictureTime;
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var posts = await _postStore.GetPicturePostsAsync();
            foreach (var post in posts)
                Posts.Add(new PicturesViewModel(post));
        }

        private async Task AddPicture()
        {
           await _pageService.PushAsync(new PicturesDetailPage(new PicturesViewModel()));
        }

        private async Task SelectPicture(PicturesViewModel post)
        {
            if (post == null)
                return;

            SelectedPost = null;
            await _pageService.PushAsync(new PicturesDetailPage(post));
        }

        private async Task DeletePicture(PicturesViewModel picturesViewModel)
        {

            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {picturesViewModel.PictureTitle}?", "Yes", "No"))
            {
                Posts.Remove(picturesViewModel);

                var contact = await _postStore.GetPicturePost(picturesViewModel.ID);
                await _postStore.DeletePicturePost(contact);
            }
        }




    }

}
