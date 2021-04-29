using WebApp.BL.BusinessModels;
using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces;

namespace WebApp.BL
{
    public class CountryService:ICountryService
    {
        private const string Postfix = " BL";

        private readonly ICountryCreatorService countryService;
        public CountryService(ICountryCreatorService countryService)
        {
            this.countryService = countryService;
        }
        public Country GetCountry()
        {
            CountryEntity countryEntity = countryService.GetCountryEntity();
            Country country = new Country() { Name = countryEntity.Name + Postfix };

            return country;
        }
    }
}
