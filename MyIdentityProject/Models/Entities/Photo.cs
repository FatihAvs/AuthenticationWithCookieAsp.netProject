using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.Models.Entities
{
    public class Photo 
    {
        public int PhotoId  { get; set; }
        public string PhotoPath { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime YuklemeTarihi { get; set; }
        public int PhotoCategory { get; set; }
        public string PhotoDescribe { get; set; }
      
    }
}
