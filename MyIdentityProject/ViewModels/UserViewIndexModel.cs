using MyIdentityProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class UserViewIndexModel
    {
        public UserViewModel userViewModel { get; set; }
        public List<Photo> Photoss { get; set; }
    }
}
