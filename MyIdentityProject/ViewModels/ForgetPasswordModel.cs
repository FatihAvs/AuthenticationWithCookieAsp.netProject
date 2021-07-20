using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class ForgetPasswordModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Girilmedi.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
