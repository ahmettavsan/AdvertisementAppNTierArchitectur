using AppAdvertisement.DTOs.ProvidedServiceDto;
using AppAdvertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Interfaces
{
    public interface IProvidedServiceService:IService<ProvidedServiceCreateDto,ProvidedServiceUpdateDto,ProvidedServiceListDto,ProvidedService>
    {

    }
}
