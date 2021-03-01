using Microsoft.AspNetCore.Mvc;
using WebApp.BL;
using WebApp.BL.BusinessModels;
using WebApp.PL.Models;

namespace WebApp.PL.Controllers
{
    public class CountryController : Controller
    {
        private const string Postfix = " PL";

        ICountryService countryService;
        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }
        public IActionResult Index()
        {
            Country country = countryService.Get();
            CountryModel countryModel = new CountryModel();
            countryModel.Name = country.Name + Postfix;

            return Content(countryModel.Name);
        }
    }
}
