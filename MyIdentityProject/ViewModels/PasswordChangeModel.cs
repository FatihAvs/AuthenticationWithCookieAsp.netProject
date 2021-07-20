using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MyIdentityProject.ViewModels
{
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Lütfen Şuanda kullanıdığınız Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şuanki Şifrenizi giriniz")]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
         public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Lütfen yeni şifrenizi giriniz")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
         public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Lütfen Yeni Şifrenizi Tekrar Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onay")]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        [Compare("NewPassword",ErrorMessage ="Yeni girdiğiniz şifreler eşleşmiyor.")]
        public string NewPasswordConfirm { get; set; }
    }
}
