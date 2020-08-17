using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostDetailViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ImageUniqueName { get; set; }
        public string ImageName { get; set; }
        public string Content { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
