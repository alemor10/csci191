using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SQLite;

namespace SocialApp.Models
{
    public class PicturePost
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string PictureTitle { get; set; }
        public string PictureCategory { get; set; }
        public string PictureLocation { get; set; }
        public string PictureTime { get; set; }
        public int PictureRating { get; set; }
        public string PicturePath { get; set; }

    }

}
