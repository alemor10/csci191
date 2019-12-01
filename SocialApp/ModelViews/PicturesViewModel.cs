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

using SocialApp.Services;

namespace SocialApp.ModelViews
{
    public class PicturesViewModel : INotifyPropertyChanged
    {
        private readonly IPicturePostService _picturePosts;
        private bool _isLoading;
        public ObservableCollection<PicturePost> Posts { get; }
        public Command SavePostCommand { get; }

        public PicturesViewModel()
        {
            Posts = new ObservableCollection<PicturePost>();

            SavePostCommand = new Command(() =>
            {
                Posts.Add(new PicturePost { PictureTitle = PictureTitle ,PictureCategory = PictureCategory, PicturePath = PicturePath, PictureRating = PictureRating });
                PictureTitle = string.Empty;

                using (StreamWriter file = File.CreateText("PicDB.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, Posts);
                }

            },
            () => !string.IsNullOrEmpty(PictureTitle));

            //EraseNotesCommand = new Command(() => Notes.Clear());

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

                SavePostCommand.ChangeCanExecute();
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





    }
}
