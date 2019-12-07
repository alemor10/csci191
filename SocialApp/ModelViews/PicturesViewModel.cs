using SocialApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.IO;

namespace SocialApp.ModelViews
{
    public class PicturesViewModel : INotifyPropertyChanged
    {
        //public ObservableCollection<PicturePost> Posts { get; }
        public Command SavePostCommand { get; }
        public Command takeImageCommand { get; }
        public PicturesViewModel()
        {
            SavePostCommand = new Command(async () => await NewPost());
            takeImageCommand = new Command(async () => PicturePath = Views.MyPage.);
            //Posts = new ObservableCollection<PicturePost>();

            /*SavePostCommand = new Command(() =>
            {
                Posts.Add(new PicturePost { PictureTitle = PictureTitle ,PictureCategory = PictureCategory, PicturePath = PicturePath, PictureRating = PictureRating });
                PictureTitle = string.Empty;
            },
            () => !string.IsNullOrEmpty(PictureTitle));*/

            //EraseNotesCommand = new Command(() => Notes.Clear());
            //Posts = await App.Database.SavePicturePostAsync();

        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        string pictureTitle;
        public string PictureTitle
        {
            get => pictureTitle;
            set
            {
                pictureTitle = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(PictureTitle)));

                //SavePostCommand.ChangeCanExecute();
            }
        }
        string pictureCategory;
        public string PictureCategory
        {
            get => pictureCategory;
            set
            {
                pictureCategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PictureCategory)));
            }
        }

        double pictureRating;
        public double PictureRating
        {
            get => pictureRating;
            set
            {
                pictureRating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PictureRating)));
            }

        }

        string picturePath;
        public string PicturePath
        {
            get => picturePath;
            set
            {
                picturePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PicturePath)));

            }
        }

        private List<PicturePost> picPosts;
        public List<PicturePost> PicPosts
        {
            get { return picPosts; }
            set
            {
                picPosts = value;
                //OnPropertyChange();
            }
        }

        // keeps the posts updated to the current ones
        private async Task CurrentPosts()
        {
            PicPosts = await App.Database.GetPicturePostAsync();
        }


        public async Task NewPost()
        {
            // get category information
            

            // get current time for post
            //var dt = System.DateTime.Now;
            //string dateTime = String.Format("{0:f}", dt);

            // get current location for post
            //var location = await Location.GetCurrentPosition();
            //var placemark = await Location.ReverseGeocodeLocation(location);
            //string address = placemark.FeatureName;

            await App.Database.SavePicturePostAsync(new PicturePost()
            {
                PictureTitle = PictureTitle,
                PictureCategory = PictureCategory,
                PicturePath = PicturePath,
                PictureRating = PictureRating
            });
            PictureTitle = string.Empty;
            PicPosts = await App.Database.GetPicturePostAsync();
        }



    }
}
