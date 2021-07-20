using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentityProject.Helper;
using MyIdentityProject.Models;
using MyIdentityProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityProject.Controllers
{
    public class HomeController : Controller
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach(var errorr in result.Errors)
                    {
                        ModelState.AddModelError("", errorr.Description);
                    } 
                }
            }

         
            return View(userViewModel);
        }
        public IActionResult LogIn(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (user != null)
                { 
                    if(await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "8 kere başarısız giriş denemesinde bulundunuz tekrar giriş " +
                            "yapabilmek için lütfen bekleyiniz.");
                        return View(loginUser);
                    }
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =  await _signInManager.PasswordSignInAsync(user,
                        loginUser.Password, loginUser.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await  _userManager.ResetAccessFailedCountAsync(user);
                        if (TempData["returnUrl"] != null)
                        {
                            return Redirect(TempData["returnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member"); 
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);
                        int basarısızGirisSayısı = await _userManager.GetAccessFailedCountAsync(user);
                        if(basarısızGirisSayısı> 7)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(25)));
                        }

                        ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    };
                   
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                }
            }
           

            return View(loginUser);
        }
        public IActionResult SifremiUnuttum()
        {
            ViewBag.IsProcessCompleted = false;
            return View();
        }
        [HttpPost]
        public IActionResult SifremiUnuttum(ForgetPasswordModel forgetPasswordModel)
        {
            
            
            AppUser user = _userManager.FindByEmailAsync(forgetPasswordModel.Email).Result;
            if (user != null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                string passwordResetLink = Url.Action("ResetPasswordConfirm","Home",new { 
                userId = user.Id, token = passwordResetToken },HttpContext.Request.Scheme);
                PasswordResetHelper.PasswordResetSendEmail(passwordResetLink,user);
                ViewBag.IsProcessCompleted = true;
                View();

              
            }
            else
            {
                ViewBag.IsProcessCompleted = false;
                ModelState.AddModelError("","Lütfen email adresinizi doğru girdinizden emin olun.");
            }


            return View(forgetPasswordModel);
        }

        public IActionResult ResetPasswordConfirm(string userId, string token)
        {
            TempData["Id"] =userId;
            TempData["token"] = token;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(PasswordResetModel passwordResetModel)
        {
            string UserId = TempData["Id"].ToString();
            string Token = TempData["token"].ToString();
            AppUser user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, Token, passwordResetModel.PasswordNew);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    ViewBag.PasswordReset = "success";

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Eposta adresinizi kontrol ediniz.");
            }

       


            return View();
        }


    }
}
