using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SocialApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;

namespace SocialApp.Views
{
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();

            takePhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No camera", ":(No camera available.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                    return;
                await DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };
            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };


            //private void ToolbarItem_Clicked(object sender, EventArgs e)
            saveButton.Clicked += async (sender, args) =>
            {
                var dte = DateTime.Now;

                PicturePost post = new PicturePost()
                {
                    PictureCategory = categoryEntry.Text,
                    //PictureRating = ratingEntry.Text,
                    PictureTime = String.Format("{0:f}", dte)
                };



                SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
                conn.CreateTable<PicturePost>();
                int rows = conn.Insert(post);
                conn.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "Category successfully inserted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Category failed to be inserted", "Ok");
                }
            };
        }
    }
}
