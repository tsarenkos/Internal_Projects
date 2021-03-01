using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.interfaces;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.Shared.Extensions;

namespace IdentityServerAspNetIdentity.Services
{
    public class FriendsService : IFriendsService
    {
        private IHttpContextAccessor _httpcontext;
        protected UserManager<ApplicationUser> _userManager;
        protected ApplicationDbContext _context;
        private IMailService _mailService;

        public FriendsService(IHttpContextAccessor httpcontext, UserManager<ApplicationUser> userManager, IMailService mailService, ApplicationDbContext context)
        {
            _httpcontext = httpcontext;
            _context = context;
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<IEnumerable<UserFriendBL>> GetMyFriendRequests()
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var Friends = _context.UserFriends.Where(uf => uf.UserId == UserId).ToList();
            var users = await _userManager.GetUsersInRoleAsync("User");
            var usersList = users.ToList();

            var query = from friend in Friends
                        join user in usersList on friend.FriendId equals user.Id
                        select new { friend = friend, friendUser = user };

            return query.Select(itm => new UserFriendBL()
            {
                UserId = itm.friend.UserId,
                FriendId = itm.friend.FriendId,
                AddDate = itm.friend.AddDate,
                IsApproved = itm.friend.IsApproved,
                Friend = new UserProfileBL()
                {
                    UserId = itm.friendUser.Id,
                    Email = itm.friendUser.Email,
                    FamilyName = itm.friendUser.FamilyName,
                    Name = itm.friendUser.Name,
                    Patronymic = itm.friendUser.Patronymic,
                    Photo = itm.friendUser.Photo,
                    Telephone = itm.friendUser.PhoneNumber,
                    UserName = itm.friendUser.UserName
                }
            });
        }

        public async Task<IEnumerable<UserFriendBL>> GetFriendRequests()
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var requests = _context.UserFriends.Where(uf => uf.FriendId == UserId && !uf.IsApproved).ToList();
            var users = await _userManager.GetUsersInRoleAsync("User");
            var usersList = users.ToList();

            var query = from request in requests
                        join user in usersList on request.UserId equals user.Id
                        select new { request = request, user = user };

            return query.Select(itm => new UserFriendBL()
            {
                UserId = itm.request.FriendId,
                FriendId = itm.user.Id,
                AddDate = itm.request.AddDate,
                IsApproved = itm.request.IsApproved,
                Friend = new UserProfileBL()
                {
                    UserId = itm.user.Id,
                    Email = itm.user.Email,
                    FamilyName = itm.user.FamilyName,
                    Name = itm.user.Name,
                    Patronymic = itm.user.Patronymic,
                    Photo = itm.user.Photo,
                    Telephone = itm.user.PhoneNumber,
                    UserName = itm.user.UserName
                }
            });
        }

        public async Task<IEnumerable<UserFriendBL>> GetFriends()
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var requests = _context.UserFriends.Where(uf => (uf.UserId == UserId || uf.FriendId == UserId) && uf.IsApproved).ToList();
            var users = await _userManager.GetUsersInRoleAsync("User");
            var usersList = users.ToList();

            var query = from request in requests
                        from user in usersList.Where(user => request.UserId == user.Id && request.UserId != UserId || request.FriendId == user.Id && request.FriendId != UserId)
                        .DefaultIfEmpty()
                        select new { request, user };

            return query.Select(itm => new UserFriendBL()
            {
                UserId = itm.request.UserId == UserId ? itm.request.FriendId : itm.request.UserId,
                FriendId = itm.request.FriendId == UserId ? itm.request.UserId : itm.request.FriendId,
                AddDate = itm.request.AddDate,
                IsApproved = itm.request.IsApproved,
                Friend = new UserProfileBL()
                {
                    UserId = itm.user.Id,
                    Email = itm.user.Email,
                    FamilyName = itm.user.FamilyName,
                    Name = itm.user.Name,
                    Patronymic = itm.user.Patronymic,
                    Photo = itm.user.Photo,
                    Telephone = itm.user.PhoneNumber,
                    UserName = itm.user.UserName
                }
            });
        }

        public async Task<IEnumerable<UserFriendBL>> GetFriendsForUser(string UserId)
        {
            var requests = _context.UserFriends.Where(uf => (uf.UserId == UserId || uf.FriendId == UserId) && uf.IsApproved).ToList();
            var users = await _userManager.GetUsersInRoleAsync("User");
            var usersList = users.ToList();

            var query = from request in requests
                        from user in usersList.Where(user => request.UserId == user.Id && request.UserId != UserId || request.FriendId == user.Id && request.FriendId != UserId)
                        .DefaultIfEmpty()
                        select new { request, user };

            return query.Select(itm => new UserFriendBL()
            {
                UserId = itm.request.UserId == UserId ? itm.request.FriendId : itm.request.UserId,
                FriendId = itm.request.FriendId == UserId ? itm.request.UserId : itm.request.FriendId,
                AddDate = itm.request.AddDate,
                IsApproved = itm.request.IsApproved,
                Friend = new UserProfileBL()
                {
                    UserId = itm.user.Id,
                    Email = itm.user.Email,
                    FamilyName = itm.user.FamilyName,
                    Name = itm.user.Name,
                    Patronymic = itm.user.Patronymic,
                    Photo = itm.user.Photo,
                    Telephone = itm.user.PhoneNumber,
                    UserName = itm.user.UserName
                }
            });
        }


        public async Task<IEnumerable<UserProfileBL>> GetNonFriends()
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var Friends = _context.UserFriends.Where(uf => uf.UserId == UserId).Select(uf => uf.FriendId).ToList();
            var Requests = _context.UserFriends.Where(uf => uf.FriendId == UserId).Select(uf => uf.UserId).ToList();
            var users = await _userManager.GetUsersInRoleAsync("User");
            var userslist = users.Where(user => user.Id != UserId).ToList();
            return userslist.Where(user => !Friends.Any(item => item == user.Id) && !Requests.Any(item => item == user.Id)).Select(user => new UserProfileBL()
            {
                UserId = user.Id,
                Email = user.Email,
                FamilyName = user.FamilyName,
                Name = user.Name,
                Patronymic = user.Patronymic,
                Photo = user.Photo,
                Telephone = user.PhoneNumber,
                UserName = user.UserName
            });
        }

        public async Task<APIResult> AddFriendRequest(UserFriendBL request)
        {
            if (request == null)
                return new APIResult() { Success = false, Error = "Аргумент request = null" };

            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);

            var model = new UserFriend()
            {
                AddDate = request.AddDate,
                IsApproved = request.IsApproved,
                FriendId = request.FriendId,
                UserId = UserId
            };
            _context.UserFriends.Add(model);
            await _context.SaveChangesAsync();

            if (user != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = user.Email,
                    Subject = $"Вам отправлен запрос на добавление в друзья от {user.UserName}",
                    Body = "Чтобы принять запрос перейдите по ссылке: <br> <a href=\"https://localhost:44347/Friends\">https://localhost:44347/Friends</a>"
                });
            }

            return new APIResult() { Success = true };
        }

        public async Task<UserFriendBL> GetFriendRequest(string FriendId)
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            if (UserId == null) return null;
            var friend = _context.UserFriends.FirstOrDefault(uf => uf.FriendId == FriendId && uf.UserId == UserId);
            if (friend == null) return null;

            var user = await _userManager.FindByIdAsync(FriendId);

            return new UserFriendBL()
            {
                UserId = friend.UserId,
                FriendId = friend.FriendId,
                AddDate = friend.AddDate,
                IsApproved = friend.IsApproved,
                Friend = new UserProfileBL()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FamilyName = user.FamilyName,
                    Name = user.Name,
                    Patronymic = user.Patronymic,
                    Photo = user.Photo,
                    Telephone = user.PhoneNumber,
                    UserName = user.UserName
                }
            };
        }

        public async Task<APIResult> AcceptFriendRequest(UserFriendBL request)
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var friend = _context.UserFriends.FirstOrDefault(uf => uf.FriendId == UserId && uf.UserId == request.FriendId);
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);
            if (friend == null)
                return new APIResult() { Success = false, Error = "Аргумент request содержит ссылку на несуществующего пользователя" };

            friend.IsApproved = true;
            friend.AddDate = request.AddDate;
            _context.UserFriends.Update(friend);
            await _context.SaveChangesAsync();

            if (user != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = user.Email,
                    Subject = "Ваш запрос на добавление в друзья был утверждён",
                    Body = "Чтобы посмотреть подробности перейдите по ссылке: <br> <a href=\"https://localhost:44347/Friends\">https://localhost:44347/Friends</a>"
                });
            }

            return new APIResult() { Success = true };
        }

        public async Task<APIResult> DeclineFriendRequest(UserFriendBL request)
        {
            var UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var friend = _context.UserFriends.FirstOrDefault(uf => uf.FriendId == UserId && uf.UserId == request.FriendId);
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);
            if (friend == null)
                return new APIResult() { Success = false, Error = "Аргумент request содержит ссылку на несуществующего пользователя" };

            _context.UserFriends.Remove(friend);
            await _context.SaveChangesAsync();

            if (user != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = user.Email,
                    Subject = "Ваш запрос на добавление в друзья был отклонён",
                    Body = "Чтобы посмотреть подробности перейдите по ссылке: <br> <a href=\"https://localhost:44347/Friends\">https://localhost:44347/Friends</a>"
                });
            }

            return new APIResult() { Success = true };
        }
    }
}