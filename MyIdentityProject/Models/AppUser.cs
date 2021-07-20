using Microsoft.AspNetCore.Identity;
using MyIdentityProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime DogumTarihi { get; set; }
        public string UserPicture { get; set; }
        public int Gender { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
