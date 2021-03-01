using Autofac;
using Autofac_App.Services;

namespace Autofac_App
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DateTimeWithOffsetService())
                            .As<IDateTimeWithOffsetService>()
                            .InstancePerLifetimeScope();
        }
    }
}
