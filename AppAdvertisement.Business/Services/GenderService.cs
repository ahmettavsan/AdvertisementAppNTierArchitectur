using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.GenderDto;
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
    public class GenderService:Service<GenderCreateDto,GenderUpdateDto,GenderListDto,Gender>,IGenderService
    {
        public GenderService(IMapper mapper,IValidator<GenderCreateDto> createDtoValidator,IValidator<GenderUpdateDto> updateDtoValidator,IUow Uow):base(mapper,createDtoValidator,updateDtoValidator,Uow)
        {

        }
    }
}
