using Filters_App.Core.Interfaces;

namespace Filters_App.Core.Services
{
    public class ChangeStringService:IChangeStringService
    {
        public string ChangeLength(string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength);
                
            }
            else
            {
                return str;
            }
        }
    }
}
