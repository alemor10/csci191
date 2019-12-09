using SocialApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SocialApp.ModelViews
{
    public class PicturesViewModel : BaseViewModel
    {

        public int ID { get; set; }
        public PicturesViewModel() { }

        public PicturesViewModel(PicturePost post)
        {
            ID = post.ID;
            _pictureTitle = post.PictureTitle;
            _pictureCategory = post.PictureCategory;
            _pictureLocation = post.PictureLocation ;
            _pictureTime  = post.PictureTime;
            _pictureRating = post.PictureRating;
            _picturePath = post.PicturePath;

        }

        private string _pictureTitle;
        public string PictureTitle
        {
            get { return _pictureTitle; }
            set
            {
                SetValue(ref _pictureTitle, value);
                OnPropertyChanged(nameof(PictureTitle));
            }
        }
        private string _pictureCategory;
        public string PictureCategory
        {
            get { return _pictureCategory; }
            set
            {
                SetValue(ref _pictureCategory, value);
                OnPropertyChanged(nameof(PictureCategory));
            }
        }

        private double  _pictureRating;
        public double PictureRating
        {
            get { return _pictureRating; }
            set
            {
                SetValue(ref _pictureRating, value);
            }

        }

        private string _picturePath;
        public string PicturePath
        {
            get { return _picturePath; }
            set
            {
                SetValue(ref _picturePath, value);

            }
        }
        private string _pictureLocation ;
        public string PictureLocation
        {
            get { return _pictureLocation; }
            set
            {
                SetValue(ref _pictureLocation, value);

            }
        }

        private string _pictureTime;
      

        public string PictureTime
        {
            get { return _pictureTime; }
            set
            {
                SetValue(ref _pictureTime, value);
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                SetValue(ref _phone, value);
            }
        }

    }
}
