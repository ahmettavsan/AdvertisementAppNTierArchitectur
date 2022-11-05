using AppAdvertisement.DTOs.AdvertisementAppUserDto;
using AppAdvertisement.DTOs.AdvertisementAppUserStatusDto;
using AppAdvertisement.DTOs.AdvertisementDto;
using AppAdvertisement.DTOs.AppRoleDto;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.DTOs.GenderDto;
using AppAdvertisement.DTOs.MilitaryStatusDto;
using AppAdvertisement.DTOs.ProvidedServiceDto;
using AppAdvertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<ProvidedServiceCreateDto, ProvidedService>().ReverseMap();
            CreateMap<ProvidedServiceUpdateDto, ProvidedService>().ReverseMap();
            CreateMap<ProvidedServiceListDto, ProvidedService>().ReverseMap();


            CreateMap<Advertisement, AdvertisementListDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementCreateDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementUpdateDto>().ReverseMap();

            CreateMap<AppUser, AppUserCreateDto>().ReverseMap();
            CreateMap<AppUser, AppUserListDto>().ReverseMap();
            CreateMap<AppUser, AppUserUpdateDto>().ReverseMap();


            CreateMap<Gender, GenderCreateDto>().ReverseMap();
            CreateMap<Gender, GenderUpdateDto>().ReverseMap();
            CreateMap<Gender, GenderListDto>().ReverseMap();

            CreateMap<AppRole, AppRoleListDto>().ReverseMap();

            CreateMap<AdvertisementAppUserStatus, AdvertisementAppUserStatusListDto>().ReverseMap();


            CreateMap<AdvertisementAppUser, AdvertisementAppUserCreateDto>().ReverseMap();
            CreateMap<AdvertisementAppUser, AdvertisementAppUserListDto>().ReverseMap();


            CreateMap<MilitaryStatus, MilitaryStatusListDto>().ReverseMap();

            //CreateMap<AdvertisementAppUser, AdvertisementAppUserCreateDto>().ReverseMap();


        }
    }
}
