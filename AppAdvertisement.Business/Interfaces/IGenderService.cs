using AppAdvertisement.DTOs.GenderDto;
using AppAdvertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Interfaces
{
    public interface IGenderService:IService<GenderCreateDto,GenderUpdateDto,GenderListDto,Gender>
    {
    }
}
