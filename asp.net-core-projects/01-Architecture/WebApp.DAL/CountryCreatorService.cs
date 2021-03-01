using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces;

namespace WebApp.DAL
{
    public class CountryCreatorService:ICountryCreatorService
    {
        public CountryEntity Get()
        {
            return new CountryEntity { Name = "Brazil" };
        }
    }
}
