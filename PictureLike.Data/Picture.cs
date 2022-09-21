using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLike.Data
{
   public  class Picture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
    }
}
