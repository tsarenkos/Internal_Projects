using System;

namespace Autofac_App.Services
{
    public interface IDateTimeWithOffsetService
    {
        DateTime GetDateTime(double offset);
    }
}
