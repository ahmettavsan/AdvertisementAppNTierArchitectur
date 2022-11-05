using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppAdvertisement.UI.Controllers
{
    public class BaseController : Controller
    {
        public AppUserCreateDto GetUserCreateDto(UserCreateModel model)
        {
            var dto = new AppUserCreateDto()
            {
                Firstname=model.Firstname,
                Surname=model.Surname,
                GenderId=model.GenderId,
                PassWord=model.PassWord,
                PhoneNumber=model.PhoneNumber,
                UserName=model.UserName,
                

            };
            return dto;
        }
    }
}
