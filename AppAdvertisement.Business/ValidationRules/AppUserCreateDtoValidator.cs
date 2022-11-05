﻿using AppAdvertisement.DTOs.AppUserDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.ValidationRules
{
    public class AppUserCreateDtoValidator:AbstractValidator<AppUserCreateDto>
    {
        public AppUserCreateDtoValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.PassWord).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
