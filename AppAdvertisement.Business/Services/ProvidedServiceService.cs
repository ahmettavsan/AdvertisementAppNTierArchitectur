using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.ProvidedServiceDto;
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
    public class ProvidedServiceService:Service<ProvidedServiceCreateDto,ProvidedServiceUpdateDto,ProvidedServiceListDto,ProvidedService> ,IProvidedServiceService
    {
        //di ile nesneler burda gelecek gelen nesnelere base gönderilecek
        //basedeki propertylere setlenecek
        public ProvidedServiceService(IMapper mapper,IValidator<ProvidedServiceCreateDto> createDtoValidator,IValidator<ProvidedServiceUpdateDto> updateDtoValidator,IUow uow):base(mapper, createDtoValidator, updateDtoValidator, uow)
        {

        }
    }
}
