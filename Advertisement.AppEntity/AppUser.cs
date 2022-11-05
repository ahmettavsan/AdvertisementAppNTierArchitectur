using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.AppEntity
{
    public class AppUser:BaseEntity
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string PhoneNumber { get; set; }
        public int  GenderId { get; set; }
        public Gender Gender { get; set; }
        public List<AppUserRole> AppUserRoles { get; set; }
        public List<AdvertisementAppUser> AdvertisementAppUsers { get; set; }


    }
}
