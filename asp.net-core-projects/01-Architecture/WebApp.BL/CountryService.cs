using WebApp.BL.BusinessModels;
using WebApp.DAL;
using WebApp.DAL.Entities;

namespace WebApp.BL
{
    public class CountryService:ICountryService
    {
        private const string Postfix = " BL";
        public Country Get()
        {
            CountryEntity countryEntity = new CountryCreatorService().Get();
            Country country = new Country();
            country.Name = countryEntity.Name + Postfix;
            return country;
        }
    }
}
