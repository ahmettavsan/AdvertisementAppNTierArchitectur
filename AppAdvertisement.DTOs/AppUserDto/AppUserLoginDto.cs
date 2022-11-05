using AppAdvertisement.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DTOs.AppUserDto
{
    public class AppUserLoginDto:IDto
    {
        public string Username { get; set; }    
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
