using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.Business.Mapping;
using AppAdvertisement.Business.Services;
using AppAdvertisement.Business.ValidationRules;
using AppAdvertisement.DataAccess.Contexts;
using AppAdvertisement.DataAccess.UnitOfWork;
using AppAdvertisement.DTOs.AdvertisementAppUserDto;
using AppAdvertisement.DTOs.AdvertisementDto;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.DTOs.GenderDto;
using AppAdvertisement.DTOs.ProvidedServiceDto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AdvertisementContext>(opt =>
            {
                opt.UseSqlServer(connectionString, opt =>
                {
                    opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AdvertisementContext)).GetName().Name);
                });
            });
            services.AddAutoMapper(typeof(MapProfile));
            services.AddScoped<IUow, Uow>();
           //---------------VALIDATORS------------------------
            services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
            services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();
            
            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisemenCreateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();
           
            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>(); 
            services.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();
            services.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementAppUserCreateDto>, AdvertisementAppUserCreateDtoValidator>();



            //-------------SERVICE----------------------------------
            services.AddScoped<IAdvertisementAppUserService, AdvertisementAppUserService>();
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IGenderService, GenderService>();
        }
    }
}
