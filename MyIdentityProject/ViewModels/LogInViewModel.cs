using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class LogInViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email Girilmedi.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lütfen Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name ="Şifre")]
        [MinLength(4,ErrorMessage ="Şifreniz en az 4 karakterli olmalıdır.")]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
