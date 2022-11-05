using AdvertisementApp.Common;
using AppAdvertisement.Business.Extensions;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.AdvertisementAppUserDto;
using AppAdvertisement.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AppAdvertisement.Business.Services
{
    public class AdvertisementAppUserService:IAdvertisementAppUserService
    {
        private readonly IUow _uOW;
        private readonly IValidator<AdvertisementAppUserCreateDto> _CreateDtoValidator;
        private readonly IMapper _mapper;

        public AdvertisementAppUserService(IUow uOW, IValidator<AdvertisementAppUserCreateDto> createDtoValidator, IMapper mapper)
        {
            _uOW = uOW;
            _CreateDtoValidator = createDtoValidator;
            _mapper = mapper;
        }
        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
          var result=  _CreateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var control = await _uOW.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.AppUserId == dto.AppUserId && x.AdvertisementId == dto.AdvertisementId);
                if (control == null)
                {
                    var entity = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uOW.GetRepository<AdvertisementAppUser>().CreateAsync(entity);
                    await _uOW.SaveChangesAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }
                List<CustomValidationError> errorList = new List<CustomValidationError> { new CustomValidationError { ErrorMessage="Daha önce başvurulan ilana tekrar başvurulamaz",PropertyName=""} };


                return new Response<AdvertisementAppUserCreateDto>(ResponseType.ValidationError,dto,errorList);
              
            }
            return new Response<AdvertisementAppUserCreateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
        }
        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            var query = _uOW.GetRepository<AdvertisementAppUser>().GetQuery();
           var list=await query.Include(x => x.Advertisement).Include(x => x.AdvertisementAppUserStatus).Include(x => x.MilitaryStatus).Include(x => x.AppUser).ThenInclude(x=>x.Gender).Where(x => x.AdvertisementAppUserStatusId == (int)type).ToListAsync();
            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }
        public async Task SetStatusAsync(int advertisementAppUserId,AdvertisementAppUserStatusType type)
        {
            //var unChanged = await _uOW.GetRepository<AdvertisementAppUser>().FindAsync(advertisementAppUserId);
            //var changed = await _uOW.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x=>x.Id==advertisementAppUserId);
            //changed.AdvertisementAppUserStatusId = (int)type;
            //_uOW.GetRepository<AdvertisementAppUser>().Update(changed,unChanged);
            var query = _uOW.GetRepository<AdvertisementAppUser>().GetQuery();
            var entity = await query.SingleOrDefaultAsync(x => x.Id == advertisementAppUserId);
            entity.AdvertisementAppUserStatusId = (int)type;

            await _uOW.SaveChangesAsync();
        }
          
    }
}
