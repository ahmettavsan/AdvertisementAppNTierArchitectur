using AdvertisementApp.Common;
using AppAdvertisement.DTOs.Interfaces;
using AppAdvertisement.DTOs.ProvidedServiceDto;
using AppAdvertisement.Entities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.Interfaces
{
    public interface IService<CreateDto,UpdateDto,ListDto,T>
        where CreateDto : class,IDto,new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity

    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto);

        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync();


        #region Note
        //bu servici providedservice için yazdığımızı düşünelim 
        //bu servici generic hale getirebilmek için providedservicecreate,update ve list kısımlarını generic hale getirmemiz lazım
        //Task<IResponse<ProvidedServiceCreateDto>> CreateAsync(ProvidedServiceCreateDto dto);
        //Task<IResponse<ProvidedServiceUpdateDto>> UpdateAsync(ProvidedServiceUpdateDto dto);

        //Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        //Task<IResponse> RemoveAsync(int id);
        //Task<IResponse<List<ProvidedServiceListDto>>> GetAllAsync();
        #endregion


    }
}
