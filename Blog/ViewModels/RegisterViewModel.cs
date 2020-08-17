using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class RegisterViewModel
    {

        [Display(Name ="Emri i përdoruesit")]
        [Required(ErrorMessage = "Ju lutem plotësoni emrin e përdoruesit!")]
        [RegularExpression(@"[a-zA-Z0-9._-]{3,30}", ErrorMessage ="Emri i përdoruesit duhet të përmbajë 3-30 karaktere, pa hapësira boshe! Karaktere speciale të lejuara janë [._-] !")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Ju lutem plotësoni emrin!")]
        [RegularExpression(@"[a-zA-Z]{1,}",ErrorMessage = "Ju lutem plotësoni saktë emrin!")]
        [Display(Name ="Emri")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Ju lutem plotësoni mbiemrin!")]
        [RegularExpression(@"[a-zA-Z]{1,}",ErrorMessage = "Ju lutem plotësoni saktë mbiemrin!")]
        [Display(Name ="Mbiemri")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Ju lutem plotësoni fjalëkalimin!")]
        [Display(Name = "Fjalëkalimi")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$",ErrorMessage = "Fjalëkalimi duhet të përmbajë nga 8 deri në 25 karaktere, të paktën një gërmë të madhe, një shifër,një karakter special!")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Ju lutem konfirmoni fjalëkalimin!")]
        [Display(Name ="Konfirmoni fjalëkalimin")]
        public string ConfirmPassword { get; set; }
    }
}