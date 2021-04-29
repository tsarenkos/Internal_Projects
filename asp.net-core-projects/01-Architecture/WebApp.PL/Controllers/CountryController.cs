using Microsoft.AspNetCore.Mvc;
using WebApp.BL;
using WebApp.BL.BusinessModels;
using WebApp.PL.Models;

namespace WebApp.PL.Controllers
{
    public class CountryController : Controller
    {
        private const string Postfix = " PL";

        private readonly ICountryService countryService;
        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }
        public IActionResult GetCountry()
        {
            Country country = countryService.GetCountry();
            CountryModel countryModel = new CountryModel() { Name = country.Name + Postfix };

            return Content(countryModel.Name);
        }
    }
}
