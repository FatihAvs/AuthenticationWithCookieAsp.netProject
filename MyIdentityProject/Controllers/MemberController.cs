using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MyIdentityProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyIdentityProject.Enums;
using System.IO;
using Microsoft.AspNetCore.Http;
using MyIdentityProject.Models.EntitiyFramework;
using Microsoft.EntityFrameworkCore;
using MyIdentityProject.Models.Entities;

namespace MyIdentityProject.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        PhotosDbContext _photosDbContext;

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, PhotosDbContext photosDbContext)
        {
            _photosDbContext = photosDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            List<Photo> photos =_photosDbContext.Photos.Where(p => p.UserId == user.Id).ToList();
            UserViewIndexModel userViewIndexModel = new UserViewIndexModel();
            userViewIndexModel.Photoss = photos;
            userViewIndexModel.userViewModel = userViewModel;
          

            return View(userViewIndexModel);
        }
        public IActionResult UserEdit()
        {
            AppUser user =_userManager.FindByNameAsync(User.Identity.Name).Result;
            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(GenderEnum)));


            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel,IFormFile picture)
        {
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(GenderEnum)));
            ModelState.Remove("password");
           
            if (ModelState.IsValid)
            {
                                                                    

                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(picture != null &&  picture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/UserPictures", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                        user.UserPicture = "/UserPictures/" + fileName;
                        

                    };
                }
                
             
                
                user.Email = userViewModel.Email;
                user.DogumTarihi = userViewModel.DogumTarihi;
               
                user.Gender = (int)userViewModel.Gender;
                user.UserName = userViewModel.UserName;
                user.PhoneNumber = userViewModel.PhoneNumber;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    ViewBag.success = "true";

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
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu.");
            }



            return View(userViewModel);
        }
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeModel passwordChangeModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, passwordChangeModel.CurrentPassword, passwordChangeModel.NewPassword);

                if (result.Succeeded && passwordChangeModel.CurrentPassword != passwordChangeModel.NewPassword)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(user, passwordChangeModel.NewPassword, true, false);
                    ViewBag.Password = "Changed";
                    return View();
                }
                else
                {
                    if (passwordChangeModel.CurrentPassword == passwordChangeModel.NewPassword)
                    {
                        ModelState.AddModelError("","Lütfen eski şifrenizden farklı bir şifre giriniz");
                    }
                    else { 
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                    }
                }



            }
            return View(passwordChangeModel);

        }
        public IActionResult PhotoUpload()
        {
            ViewBag.PhotoCategory = new SelectList(Enum.GetNames(typeof(CategoryEnum)));
            return View();

          

        }
        [HttpPost]
        public async Task<IActionResult> PhotoUpload(PhotoUploadModel photoUploadModel,IFormFile picture)
        {
            ViewBag.PhotoCategory = new SelectList(Enum.GetNames(typeof(CategoryEnum)));
            if (ModelState.IsValid)
            {


                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (picture != null && picture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/UserUploadPictures", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                        Photo photo = new Photo();
                        photo.PhotoPath = "/UserUploadPictures/" + fileName;
                        photo.UserId = user.Id;
                        photo.YuklemeTarihi = DateTime.Today;
                        photo.PhotoCategory = (int)photoUploadModel.Category;
                        photo.PhotoDescribe = photoUploadModel.PhotoDescribe;

                        var addedEntity = _photosDbContext.Entry(photo);
                        addedEntity.State = EntityState.Added;
                        _photosDbContext.SaveChanges();
                        ViewBag.Success = "Success";

                       
                    };
                }



            }
           return View();

        }
        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }
        public IActionResult Sorular()
        {
             var soru = _photosDbContext.Soru.Where(i => i.SoruId == 1).FirstOrDefault();
            List<Choice> choices = _photosDbContext.Choice.Where(i => i.SoruId == 1).ToList();
            QuesitonViewModel quesitonViewModel = new QuesitonViewModel();
            quesitonViewModel.Secenekler = choices;
            quesitonViewModel.Soruu = soru;


            return View(quesitonViewModel);
        }

    }
}