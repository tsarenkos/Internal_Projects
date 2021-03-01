using System;

namespace Ninject_App.Services
{
    public interface IDateTimeWithOffsetService
    {
        DateTime GetDateTime(double offset);
    }
}
