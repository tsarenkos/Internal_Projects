using System.Threading.Tasks;
using Claims_App.Data;
using Claims_App.IdentityServices;
using Claims_App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Claims_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IUserService userService;
        private readonly ApplicationDbContext context;

        public AccountController(IRoleService roleService, IUserService userService, ApplicationDbContext context)
        {
            this.roleService = roleService;
            this.userService = userService;
            this.context = context;
        }

        [Route("account/init")]
        [Authorize(Policy = "Belarus")]
        public async Task<IActionResult> InitRoles()
        {
            await roleService.InitRoles();

            return RedirectToAction("GetRoles");
        }

        [Route("account/getroles")]
        [Authorize(Policy = "phone or email confirmed")]
        public async Task<IActionResult> GetRoles()
        {
            return View(await roleService.GetRoles());
        }

        [Route("account/addrole")]
        [Authorize(Policy = "access failed limit")]
        public async Task<IActionResult> AddRole(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            await roleService.AddRole(name);
            
            return RedirectToAction("GetRoles");
        }

        [Route("account/adduser")]
        [Authorize(Roles = "Admin")]
        [HttpGet]        
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await userService.AddUser(model.Email, model.Password, model.Country);
            }
            return RedirectToAction("Index","Home");
        }

        [Route("account/addusertorole")]        
        [HttpGet]
        public IActionResult AddUserToRole()
        {
            ViewBag.Roles = new SelectList(context.Roles, "Name", "Name");
            ViewBag.Users = new SelectList(context.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await userService.AddUserToRole(model.UserId, model.RoleName);
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("account/getuserroles")]
        [Authorize(Policy = "phone exists")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }
            return View(await userService.GetUserRoles(email));
        }
    }
}
