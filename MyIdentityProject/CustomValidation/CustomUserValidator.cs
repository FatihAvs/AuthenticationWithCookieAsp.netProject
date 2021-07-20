using Microsoft.AspNetCore.Identity;
using MyIdentityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.CustomValidation
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {

            List<IdentityError> errors = new List<IdentityError>();
            string[] rakamlar = new string[]{"0","1","2","3","4","5","6","7","8","9" };
            var a =  rakamlar.Where(i => i == user.UserName[0].ToString());
            if (a.Count() > 0)
            {
                errors.Add(new IdentityError() { Code = "UserNameCantStartWithNumber", Description = "Kullanıcı adı rakamla başlayamaz." });
            }
            if (errors.Count() == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
