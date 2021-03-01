using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Factory.WEB.IdentityServices;


namespace Factory.UI.Controllers
{    
    public class AdminController : Controller
    {
        private readonly IRoleService roleService;

        public AdminController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
                
        [Route("initroles")]
        public async Task<IActionResult> InitRoles()
        {
            await roleService.InitRoles();

            return RedirectToAction("GetRoles");
        }

        [Route("getroles")]
        public async Task<IActionResult> GetRoles()
        {            
            return View(await roleService.GetRoles());
        }

        [Route("addrole")]
        public async Task<IActionResult> AddRole(string name)
        {
            if (name != null)
            {
                await roleService.AddRole(name);
            }            
            return RedirectToAction("GetRoles");
        }
    }
}
