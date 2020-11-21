using DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IEmployeesService employeesService;

        public EmployeesController(IEmployeesService employeesService, IUserService userService, ITownService townService) : base(userService, townService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> Index(bool assigned = true)
        {
          //  ViewData["Assigned"] = assigned;
            if (assigned)
            {
                ViewData["Headline"] = "Occupied employees";
            }
            else
            {
                ViewData["Headline"] = "Unoccupied employees";
            }

            ICollection<EmployeeMiniDTOout> employees = await employeesService.GetEmployeesMiniAsync(assigned);
            return View(employees);
        }

        public async Task<IActionResult> Search(EmployeeSearchQuery dto)
        {
            if (!dto.Options.Any())
            {
                return RedirectToAction("Index");
            }
            ICollection<EmployeeMiniDTOout> employeesFd = await employeesService.GetSearchedEmployeesAsync(dto);
            ViewData["Headline"] = $"Results of search for {dto.ToString()}";
            return View("Index",employeesFd);
        }
    }
}
