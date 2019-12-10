
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
    public class PicturesPageViewModel : BaseViewModel
    {
        private PicturesViewModel _selectedPost;
        private IPicturePostStore _postStore;
        private IPageService _pageService;

        private bool _isDataLoaded;




        public ObservableCollection<PicturesViewModel> Posts
        { get;  set; }
        = new ObservableCollection<PicturesViewModel>();

        public PicturesViewModel SelectedPost
        {
            get { return _selectedPost; }
            set { SetValue(ref _selectedPost, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadByTime { get; private set; }
        public ICommand LoadByRating { get; private set; }
        public ICommand LoadByBusiness { get; private set; }
        public ICommand LoadByPersonal { get; private set; }
        public ICommand LoadByEducational { get; private set; }
        public ICommand LoadAllPost { get; private set; }



        public ICommand AddPictureCommand { get; private set; }
        public ICommand SelectPictureCommand { get; private set; }
        public ICommand DeletePictureCommand { get; private set; }
        public ICommand ResetPictureRatingCommand { get; private set; }
        public ICommand ResetPictureCategoryCommand { get; private set; }


        public PicturesPageViewModel(IPicturePostStore pictureStore, IPageService pageService)
        {
            _postStore = pictureStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadAllPost = new Command (async () =>  await ReloadData());

            LoadByTime = new Command(async () =>  LoadTime());
            LoadByRating = new Command(async () => LoadRating());
            LoadByBusiness = new Command(async () => LoadBusiness());
            LoadByPersonal = new Command(async () => LoadPersonal());
            LoadByEducational = new Command(async () => LoadEducational());



            AddPictureCommand = new Command(async () => await AddPicture());
            SelectPictureCommand = new Command<PicturesViewModel>(async c => await SelectPicture(c));
            DeletePictureCommand = new Command<PicturesViewModel>(async c => await DeletePicture(c));
            ResetPictureRatingCommand = new Command<PicturesViewModel>(async (obj) => await ResetPictureRating(obj));
            ResetPictureCategoryCommand = new Command<PicturesViewModel>(async (obj) => await ResetPictureCategory(obj));

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
            foreach (PicturePost post in posts)
                Posts.Add(new PicturesViewModel(post));
        }
        private async Task ReloadData()
        {
            var posts = await _postStore.GetPicturePostsAsync();
            foreach (PicturePost post in posts)
                Posts.Add(new PicturesViewModel(post));
        }

        private async Task LoadTime()
        {
            Posts = new ObservableCollection<PicturesViewModel>(from i in Posts orderby i.PictureRating select i);
        }

        private async Task LoadRating()
        {
            var counter = 0;

            foreach(var p in Posts)
            {
                if (p.PictureRating.Equals(8))
                {
                    if (counter > 4 && p.PictureRating.Equals(8))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if(p.PictureRating.Equals(7))
                {
                    if (counter > 4 && p.PictureRating.Equals(7))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(6))
                {
                    if (counter > 4 && p.PictureRating.Equals(6))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(5))
                {
                    if (counter > 4 && p.PictureRating.Equals(5))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(4))
                {
                    if (counter > 4 && p.PictureRating.Equals(4))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(3))
                {
                    if (counter > 4 && p.PictureRating.Equals(3))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(2))
                {
                    if (counter > 4 && p.PictureRating.Equals(2))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else if (p.PictureRating.Equals(1))
                {
                    if (counter > 4 && p.PictureRating.Equals(1))
                    {
                        Posts.Remove(p);
                    }
                    else
                    {
                        counter++;
                    }
                }
                else
                {
                    Posts.Remove(p);
                }
            }
        }
        private async Task LoadBusiness()
        {
            foreach (var p in Posts)
            {
                if (p.PictureCategory.Equals("Business"))
                {
                }
                else
                {
                    Posts.Remove(p);
                }
            }

        }

        private async Task LoadPersonal()
        {
            foreach (var p in Posts)
            {
                if(p.PictureCategory.Equals("Personal"))
                {
                }
                else
                {
                    Posts.Remove(p);
                }
            }
        }


        private async Task LoadEducational()
        {
            foreach (var p in Posts)
            {
                if(p.PictureCategory.Equals("Educational"))
                {
                }
                else
                {
                    Posts.Remove(p);
                }
            }

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

        private async Task ResetPictureRating (PicturesViewModel post)
        {
            post.PictureRating= 0;
        }


        private async Task ResetPictureCategory(PicturesViewModel post)
        {
            post.PictureCategory = "";
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
