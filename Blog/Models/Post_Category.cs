using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post_Category
    {
        public int Post_CategoryId { get; set; }
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
    }
}
