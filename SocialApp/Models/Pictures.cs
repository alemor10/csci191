﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SocialApp.Models
{
    public class PicturePost
    {
        public string PictureTitle { get; set; }
        public string PictureCategory { get; set; }
        public string PictureLocation { get; set; }
        public string PictureTime { get; set; }
        public double PictureRating { get; set; }
        public byte[] PictureArray { get; set  }

    }

}
