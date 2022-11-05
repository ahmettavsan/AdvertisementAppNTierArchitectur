using AdvertisementApp.Common;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DTOs.AdvertisementAppUserDto;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.DTOs.MilitaryStatusDto;
using AppAdvertisement.Entities;
using AppAdvertisement.UI.Extensions;
using AppAdvertisement.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAdvertisement.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService appUserService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _appUserService = appUserService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {

            return View();
        }
        [Authorize(Roles="Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
            var userId=int.Parse((User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)).Value);
            var userInfo = await _appUserService.GetByIdAsync<AppUserListDto>(userId);
           
            ViewBag.GenderId = userInfo.Data.GenderId;
            var items = Enum.GetValues(typeof(MilitaryStatusType));
            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto
                {
                    Id=item,
                    Definition=Enum.GetName(typeof(MilitaryStatusType),item),
                });

                
            }
            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");


            return View(new AdvertisementAppUserCreateModel{
                AdvertisementId=advertisementId,
                AppUserId=userId,
            });
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            AdvertisementAppUserCreateDto dto = new();
            if (model.File!=null)
            {
                var dosyaadi = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.File.FileName);
                var dosya = dosyaadi + extName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", dosya);
                var stream = new FileStream(path,FileMode.Create);
               await model.File.CopyToAsync(stream);
                dto.CvPath=path;
            }
            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.AdvertisementId = model.AdvertisementId;
            dto.AppUserId = model.AppUserId;
            dto.EndDate = model.EndDate;
            dto.MilitaryStatusId = model.MilitaryStatusId;
            dto.WorkExperience = model.WorkExperience;
           var response=await  _advertisementAppUserService.CreateAsync(dto);
            if (response.ResponseType==ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
                var userInfo = await _appUserService.GetByIdAsync<AppUserListDto>(userId);
                var items = Enum.GetValues(typeof(MilitaryStatusType));
                var list = new List<MilitaryStatusListDto>();
                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto
                    {
                        Id = item,
                        Definition = Enum.GetName(typeof(MilitaryStatusType), item),
                    });


                }
                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");

                ViewBag.GenderId = userInfo.Data.GenderId;
                return View(model);
            }
            return RedirectToAction("HumanResource", "Home");

            
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> List()
        {
            var list =await  _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Basvurdu);

            return View(list);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SetStatus(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            await _advertisementAppUserService.SetStatusAsync(advertisementAppUserId,type);
            return RedirectToAction("List");
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ApprovedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Mulakat);
            return View(list);

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Olumsuz);
            return View(list);

        }
    }
}
