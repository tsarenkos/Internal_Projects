using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface IMailService
     {
         Task SendAsync(MailModelBL mailModel);
     }
}
