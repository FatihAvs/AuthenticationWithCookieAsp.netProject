using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.CustomValidation
{
    public class CustomIdentitiyErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DublicateEmail",
                Description = $"Bu email {email} kullanılmaktadır."
            };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordIsTooShort",
                Description = "Parola uzunluğu çok kısa"
            };
        }



    }
}
