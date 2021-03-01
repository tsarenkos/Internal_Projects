using Ninject_App.Services;
using Ninject.Modules;

namespace Ninject_App
{
    public class NinjectRegistrations:NinjectModule
    {
        public override void Load()
        {
            Bind<IDateTimeWithOffsetService>().To<DateTimeWithOffsetService>();
        }
    }
}