using SocialApp.Models; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace SocialApp.ModelViews
{
    public class PicturesViewModel : INotifyPropertyChanged
    {
        private PicturePost post = new PicturePost();

        public PicturePost PicturePost
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (PicturePost.PictureTitle == "Hello" && PicturePost.PictureCategory == "1")
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "Successfully Login", "Okay");
                        // Open next page
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "Error Login", "Okay");
                    }
                });
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}