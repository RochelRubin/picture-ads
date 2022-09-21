using PictureLike.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureLike.Web.Models
{
    public class PictureViewModel
    {
        public List<Picture> Pictures { get; set; }
        public Picture Picture { get; set; }
        public List<int> AlreadySaw { get; set; }
    }
}
