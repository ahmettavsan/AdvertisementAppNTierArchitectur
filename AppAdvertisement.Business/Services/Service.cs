using AdvertisementApp.Common;
using AppAdvertisement.Business.Extensions;
using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.Interfaces;
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
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto,T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity

    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUow _uOW;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uOW)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uOW = uOW;
        }

        
        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
               await  _uOW.GetRepository<T>().CreateAsync(createdEntity);
              await  _uOW.SaveChangesAsync();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(ResponseType.ValidationError,dto,result.ConvertToCustomValidationError());
          
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uOW.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uOW.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data==null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id}'ye sahip data bulunamadı");
               

            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);

        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uOW.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data==null)
            {
                return new Response(ResponseType.NotFound, $"{id}'ye sahip data bulunamadı");

            }
            _uOW.GetRepository<T>().Remove(data);
            await _uOW.SaveChangesAsync();

            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var unChangedData = await _uOW.GetRepository<T>().FindAsync(dto.Id);
                if (unChangedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id}'ye sahip data bulunamadı");
                }
                var entity = _mapper.Map<T>(dto);
                _uOW.GetRepository<T>().Update(entity, unChangedData);
                await _uOW.SaveChangesAsync();

                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());


        }
    }
}
