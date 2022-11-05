using AdvertisementApp.Common;
using AppAdvertisement.Business.Extensions;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.Business.ValidationRules;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.AppRoleDto;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Services
{
    public class AppUserService:Service<AppUserCreateDto,AppUserUpdateDto,AppUserListDto,AppUser>,IAppUserService
    {
        private readonly IValidator<AppUserCreateDto> _createValidator;
        private readonly IMapper _mapper;
        private readonly IUow _uOW;
        private readonly IValidator<AppUserLoginDto> _loginValidator;

        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uOW, IValidator<AppUserLoginDto> loginValidator) : base(mapper, createDtoValidator, updateDtoValidator, uOW)
        {
            _createValidator = createDtoValidator;
            _mapper = mapper;
            _uOW = uOW;
            _loginValidator = loginValidator;
        }
        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto,int roleId)
        {
            var result =  _createValidator.Validate(dto);
            if (result.IsValid)
            {
                var entity = _mapper.Map<AppUser>(dto);
                await _uOW.GetRepository<AppUser>().CreateAsync(entity);
                await _uOW.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                {
                    AppUser = entity,
                    AppRoleId = roleId

                });
                await _uOW.SaveChangesAsync();
                return new Response<AppUserCreateDto>(ResponseType.Success,dto);
            }
            return new Response<AppUserCreateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<AppUserListDto>> ChechUser(AppUserLoginDto dto)
        {
            var validationResult=_loginValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user = await _uOW.GetRepository<AppUser>().GetByFilterAsync(x => x.UserName == dto.Username && x.PassWord == dto.Password);
                if (user!=null)
                {
                    var appUserDto = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success,appUserDto);
                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı Adı veya Şifre Hatalıdır");

            }
            return new Response<AppUserListDto>(ResponseType.ValidationError,"Kullanıcı adı veya şifre boş olamaz");

        }
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {
            //appuser tablosunda benim gönderdiğim id ye sahip kullanıcı varsa datalarını getir
            var roles=await _uOW.GetRepository<AppRole>().GetAllAsync(x => x.AppUserRoles.Any(x => x.AppUserId == userId));
            if (roles==null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "Not Found");
            }
            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            return new Response<List<AppRoleListDto>>(ResponseType.Success,dto);
        }
    }
}
