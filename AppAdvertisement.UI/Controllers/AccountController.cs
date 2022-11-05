using AdvertisementApp.Common;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.UI.Extensions;
using AppAdvertisement.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAdvertisement.UI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IAppUserService _appUserService;
        private readonly IValidator<AppUserLoginDto> _appUserLoginValidator;
        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IAppUserService appUserService, IValidator<AppUserLoginDto> appUserLoginValidator)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
            _appUserService = appUserService;
            _appUserLoginValidator = appUserLoginValidator;
        }

        public async Task<IActionResult> SignUp()
        { 
           var genderList= await _genderService.GetAllAsync();
            var model = new UserCreateModel();
            model.Genders = new SelectList(genderList.Data,"Id","Definition");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel user)
        {
            var result  =await _userCreateValidator.ValidateAsync(user);
            if (result.IsValid)
            {
                var dto = GetUserCreateDto(user);
                var responseService=await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);
                return this.ResponseRedirectAction(responseService, "SignIn", "Account");

            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            user.Genders = new SelectList(response.Data, "Id", "Definition",user.GenderId);
            return View(user);

        }
        public IActionResult SignIn()
        {
            return View(new AppUserLoginDto());
        }
        [HttpPost]
        public  async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {
            var result = await _appUserService.ChechUser(dto);
            if (result.ResponseType==ResponseType.Success)
            {
              var roleResult= await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
                var claims = new List<Claim>();
                if (roleResult.ResponseType==ResponseType.Success)
                {
                    foreach (var role in roleResult.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Definition));
                    }
                   
                }
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("Kullancı Adı veya Şifre Hatalıdır", result.Message);
            return View(dto);
           
           
        }
        public async Task<IActionResult> Logout()
        {
            
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
