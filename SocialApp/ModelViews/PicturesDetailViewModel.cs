using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SocialApp.Models;
using SocialApp.ModelViews;
using SocialApp.Services;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace SocialApp.ModelViews
{
    public class PictureDetailViewModel
    {
        private readonly IPicturePostStore _pictureStore;
        private readonly IPageService _pageService;

        public PicturePost Post { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand PickPictureCommand { get; private set; }


        public PictureDetailViewModel(PicturesViewModel viewModel, IPicturePostStore pictureStore , IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pictureStore = pictureStore;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());
            PickPictureCommand = new Command(async () => await PickPicture());

            Post = new PicturePost
            {
                ID = viewModel.ID,
                PictureTitle = viewModel.PictureTitle,
                PictureCategory = viewModel.PictureCategory,
                PictureLocation = viewModel.PictureLocation,
                PictureTime = viewModel.PictureTime,
                PictureRating = viewModel.PictureRating,
                PicturePath = viewModel.PicturePath
            };
        }
        //&& String.IsNullOrWhiteSpace(Post.PictureCategory
        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Post.PictureTitle))
            {
                await _pageService.DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            if (Post.ID == 0)
            {
                await _pictureStore.AddPicturePost(Post);
                MessagingCenter.Send(this, Events.PostAdded, Post);
            }
            else
            {
                await _pictureStore.UpdatePicturePost(Post);
                MessagingCenter.Send(this, Events.PostAdded, Post);
            }
            await _pageService.PopAsync();
        }

        async Task PickPicture()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            if (file == null)
                return;

            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});
        }

    }
}

