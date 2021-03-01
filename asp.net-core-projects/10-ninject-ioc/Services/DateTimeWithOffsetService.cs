using System;

namespace Ninject_App.Services
{
    public class DateTimeWithOffsetService:IDateTimeWithOffsetService
    {
        public DateTime GetDateTime(double offset)
        {
            return DateTime.UtcNow.AddHours(offset);
        }
    }
}