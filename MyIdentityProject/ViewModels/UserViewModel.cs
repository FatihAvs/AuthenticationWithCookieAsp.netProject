using MyIdentityProject.Enums;
using MyIdentityProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class UserViewModel
    {
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı ismi girilmelidir.")]
        public string UserName { get; set; }
     
        [Display(Name="Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Display(Name ="Email")]
        [Required(ErrorMessage = "Email girilmelidir.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password girilmelidir.")]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        public string UserPicture { get; set; }
        [Display(Name = "Cinsiyet")]
        public GenderEnum Gender { get; set; }
        [Display(Name = "Dogum Tarihi")]
        [DataType(DataType.Date)]
        public  DateTime  DogumTarihi { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
