using Autofac;
using WebApp.DAL;
using WebApp.DAL.Interfaces;

namespace WebApp.BL.Infrastructure
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new CountryCreatorService())
                .As<ICountryCreatorService>()
                .InstancePerLifetimeScope();
        }
    }
}
