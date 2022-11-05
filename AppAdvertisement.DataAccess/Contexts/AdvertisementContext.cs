using AppAdvertisement.DataAccess.Configurations;
using AppAdvertisement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DataAccess.Contexts
{
    public class AdvertisementContext:DbContext
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfiguration(new AdvertisementAppUserConfiguration());--->bu şekilde tek tek vermek yerine
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//çalıştığın assemblyde configurationlar git ordan al
            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementAppUser> AdvertisementAppUsers { get; set; }
        public DbSet<AdvertisementAppUserStatus> AdvertisementAppUserStatuses { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<MilitaryStatus> MilitaryStatuses { get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }

    }
}
