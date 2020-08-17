using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int PostId { get; set; }
        public string UniqueImgName { get; set; }

        public virtual Post Post { get; set; }
    }
}
