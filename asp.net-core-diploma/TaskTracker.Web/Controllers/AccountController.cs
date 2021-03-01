using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskTracker.Models;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Web.Models;

namespace TaskTracker.Account.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration Configuration;
        private readonly IMyTokenService _tokenservice;
        private readonly IIdentityClient _client;

        public AccountController(IMyTokenService tokenservice, IHttpContextAccessor context, IConfiguration configuration, IIdentityClient client)
        {
            _context = context;
            Configuration = configuration;
            _tokenservice = tokenservice;
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                APIToken token = await _tokenservice.GetIdentityToken(new LoginViewModel()
                {
                    UserName = "Dwain",
                    Password = "Dwain"
                });
                if (!token.Success)
                    throw new System.Exception(token.Error);

                var model = new RegisterUserBL()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                };
                APIResult res = await _client.CreateUser<RegisterUserBL>("api/createuser", model, token.token);
                if (res.Success)
                {
                    var tokenmodel = new LoginViewModel()
                    {
                        UserName = user.UserName,
                        Password = user.Password
                    };
                    token = await _tokenservice.GetIdentityToken(tokenmodel);
                    if (!token.Success)
                        throw new System.Exception(token.Error);

                    token = await _tokenservice.GetAPIToken(tokenmodel);
                    if (!token.Success)
                        throw new System.Exception(token.Error);

                    return LocalRedirect("/Profile");
                }
                else
                {
                    ModelState.AddModelError("", res.Error);
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                APIToken token = await _tokenservice.GetIdentityToken(user);
                if (!token.Success)
                {
                    ModelState.AddModelError(string.Empty, "Неправильное имя пользователя либо пароль");
                    return View(user);
                }
                token = await _tokenservice.GetAPIToken(user);
                if (!token.Success)
                {
                    ModelState.AddModelError(string.Empty, "Неправильное имя пользователя либо пароль");
                    return View(user);
                }

                if (user.ReturnUrl != null)
                {
                    return LocalRedirect(user.ReturnUrl);
                }
                else
                {
                    return LocalRedirect("/MyTask/Index");
                }
            }

            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            _tokenservice.ClearTokens();
            Response.Cookies.Delete("UserName");

            return LocalRedirect("/");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                APIResult res = await _client.Post<ChangePasswordModel>("api/changepassword", model);
                if (res.Success)
                {
                    ViewBag.ChangePassword = true;
                    return View();
                }
                else
                    ModelState.AddModelError("", res.Error);
            }
            return View(model);
        }
    }
}
 