using AppAdvertisement.DTOs.AdvertisementAppUserStatusDto;
using AppAdvertisement.DTOs.AdvertisementDto;
using AppAdvertisement.DTOs.AppUserDto;
using AppAdvertisement.DTOs.MilitaryStatusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DTOs.AdvertisementAppUserDto
{
    public class AdvertisementAppUserListDto
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public AdvertisementListDto Advertisement { get; set; }
        public int AppUserId { get; set; }
        public AppUserListDto AppUser { get; set; }
        public int AdvertisementAppUserStatusId { get; set; }
        public AdvertisementAppUserStatusListDto AdvertisementAppUserStatus { get; set; }
        public int MilitaryStatusId { get; set; }
        public MilitaryStatusListDto MilitaryStatus { get; set; }
        public DateTime? EndDate { get; set; }

        public int WorkExperence { get; set; }
        public string CvPath { get; set; }

    }
}
