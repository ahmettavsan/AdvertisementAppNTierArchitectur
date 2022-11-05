using Advertisement.AppEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Contexts
{
    public class AdvertisementContext:DbContext
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options):base(options)
        {

        }
        public DbSet<> Advertisements { get; set; }
        public DbSet<AdvertisementAppUser> AdvertisementAppUsers { get; set; }



    }
}
