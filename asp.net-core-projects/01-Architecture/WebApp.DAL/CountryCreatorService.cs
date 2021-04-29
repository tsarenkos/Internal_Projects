using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces;

namespace WebApp.DAL
{
    public class CountryCreatorService:ICountryCreatorService
    {
        private const string Country = "Brazil";
        public CountryEntity GetCountryEntity()
        {
            return new CountryEntity { Name = Country };
        }
    }
}
