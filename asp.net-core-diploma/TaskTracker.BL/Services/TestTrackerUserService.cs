using System.Collections.Generic;
using System.Linq;
using TaskTracker.BL.Interfaces;
using TaskTracker.BL.Models;
using TaskTracker.DAL.Models;

namespace TaskTracker.BL.Services
{
    public class TestTrackerUserService : IGenericListBL<TaskTrackerUserBL>
    {
        ApplicationDbContext _context;

        public TestTrackerUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskTrackerUserBL> GetAll()
        {
            return _context.Users.Select(x => new TaskTrackerUserBL()
            {
                UserId = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                UserName = x.UserName
            });
        }
    }
}
