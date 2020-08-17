using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public User MyProperty { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
    
}
