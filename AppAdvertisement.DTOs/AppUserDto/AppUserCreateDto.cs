using AppAdvertisement.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DTOs.AppUserDto
{
    public class AppUserCreateDto:IDto
    { 
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
    }
}
