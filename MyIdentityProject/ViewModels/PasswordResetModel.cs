using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class PasswordResetModel
    {
   
        [Required(ErrorMessage = "Password girilmelidir.")]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string PasswordNew { get; set; }
    }
}
