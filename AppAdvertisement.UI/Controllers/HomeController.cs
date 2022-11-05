using AppAdvertisement.Business.Interfaces;
using AppAdvertisement.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAdvertisement.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IProvidedServiceService _providedService;

        public HomeController(IProvidedServiceService providedService, IAdvertisementService advertisementService)
        {
            _providedService = providedService;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _providedService.GetAllAsync();
            return this.ResponseView(response);
          
        }
        public async Task<IActionResult> HumanResource()
        {
          var response=  await _advertisementService.GetActivesAsync();

            return this.ResponseView(response);
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
