﻿using AppAdvertisement.DTOs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DTOs.AppRoleDto
{
    public class AppRoleCreateDto:IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
