using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        [Display(Name ="Titulli")]
        public string Title { get; set; }
      
        [Display(Name ="Imazhi")]
        public IFormFile Image { get; set; }
      
        [Display(Name = "Përmbajtja")]
        public string Content { get; set; }
       
        [Display(Name = "Kategoria")]
        public List<int?> CategoryID { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
