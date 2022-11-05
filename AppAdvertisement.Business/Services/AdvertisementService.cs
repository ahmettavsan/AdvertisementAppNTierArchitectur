using AdvertisementApp.Common;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.AdvertisementDto;
using AppAdvertisement.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Services
{
   
    public class AdvertisementService : Service<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
    {
        private readonly IUow _uOw;
        private readonly IMapper _mapper;
        //di aracılığıyla nesneler buraya gelecek gelen nesneler service gönderilecek base() ile
        public AdvertisementService(IMapper mapper,IValidator<AdvertisementCreateDto> creteDtoValidator,IValidator<AdvertisementUpdateDto> updateDtoValidator,IUow uow):base(mapper, creteDtoValidator, updateDtoValidator, uow)
        {
            _uOw = uow;
            _mapper = mapper;
        }
        public async Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync()
        {
          var data= await _uOw.GetRepository<Advertisement>().GetAllAsync(x => x.Status==true, OrderByType.Desc);
            var dto = _mapper.Map<List<AdvertisementListDto>>(data);
            return new Response<List<AdvertisementListDto>>(ResponseType.Success, dto);
        }
    }
}
