using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SocialApp.Models;
using SocialApp.ModelViews;
using SocialApp.Services;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.IO;
using Xamarin.Essentials;
using System.Globalization;

namespace SocialApp.ModelViews
{
    public class PictureDetailViewModel
    {
        //declaration of services for this page 
        private  IPicturePostStore _pictureStore;
        private  IPageService _pageService;


        //Post that is displayed
        public PicturePost Post { get; private set; }

        //commands used in the page

        public ICommand SaveCommand { get; private set; }
        public ICommand PickPictureCommand { get; private set; }
        public ICommand SwipeCommand { get; private set; }

        //constructor 
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
        //save post to db
        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Post.PictureTitle))
            {
                await _pageService.DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            if (Post.ID.ToString() == null )
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
        

        // handles the user picking a picture from their library 
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
            {
                return;
            }
            //grab the user path
            Post.PicturePath = file.Path.ToString();
            // grab the time 
           Post.PictureTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //grab location
            Plugin.Media.Abstractions.Location imageLocation;
            imageLocation = new Plugin.Media.Abstractions.Location();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    imageLocation.Latitude = location.Latitude;
                    imageLocation.Longitude = location.Longitude;
                    Post.PictureLocation = "(" + imageLocation.Latitude + ", " + imageLocation.Longitude + ")";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }


    }
}

