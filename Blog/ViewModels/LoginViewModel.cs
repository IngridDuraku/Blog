using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Ju lutem plotësoni emrin e përdoruesit!")]
        [Display(Name ="Emri i përdoruesit")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Ju lutem plotësoni fjalëkalimin!")]
        [Display(Name ="Fjalëkalimi")]
        public string Password { get; set; }
    }
}
