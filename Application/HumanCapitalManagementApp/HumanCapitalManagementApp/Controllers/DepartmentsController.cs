using DTO;
using System.Linq;
using Services.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HumanCapitalManagementApp.Controllers
{
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentsService departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService, IUserService userService) : base(userService)
        {
            this.departmentsService = departmentsService;
        }
        public async Task<IActionResult> Index()
        {
            if (UserService.IsLoggedIn)
            {
                List<DepartmentDTOout> deps = (await departmentsService.GetDepartmentsAsync()).ToList();
                return View(deps);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}