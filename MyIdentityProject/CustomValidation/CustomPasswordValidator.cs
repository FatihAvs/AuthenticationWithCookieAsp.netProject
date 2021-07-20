using Microsoft.AspNetCore.Identity;
using MyIdentityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.CustomValidation
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> error = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                error.Add(new IdentityError() { Code = "PasswordContainUsername" , Description = "Şifre Kullanıcı Adı içermemelidir."});
            };
            if (password.ToLower().Contains("1234"))
            {
                error.Add(new IdentityError() { Code = "PasswordContains1234", Description = "Şifre 1234 gibi ardışık rakamlar içermemelidir." });
            };
            if (error.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(error.ToArray()));
            }
        
        }
    }
}
