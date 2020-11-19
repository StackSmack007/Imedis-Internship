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

        public DepartmentsController(IDepartmentsService departmentsService, IUserService userService, ITownService townService) : base(userService, townService)
        {
            this.departmentsService = departmentsService;
        }
        public async Task<IActionResult> Index()
        {
            List<DepartmentDTOout> deps = (await departmentsService.GetDepartmentsAsync()).ToList();
            return View(deps);
        }
    }
}
