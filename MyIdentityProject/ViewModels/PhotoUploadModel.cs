using MyIdentityProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.ViewModels
{
    public class PhotoUploadModel
    {
       
        public string PhotoPath { get; set; }
        [DataType(DataType.Text)]
        [StringLengthAttribute(1000,ErrorMessage ="Açıklama en fazla 1000 karakter içerebilir.")]
        public string PhotoDescribe { get; set; }
        public CategoryEnum Category { get; set; }
        
    }
}
