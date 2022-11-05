using AppAdvertisement.DTOs.AdvertisementDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.ValidationRules
{
    public class AdvertisemenCreateDtoValidator:AbstractValidator<AdvertisementCreateDto>
    {
        public AdvertisemenCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
