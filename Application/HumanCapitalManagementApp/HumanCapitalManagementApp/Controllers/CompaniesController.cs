using DTO;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly ICompaniesService companiesService;
        private readonly ITownService townService;

        public CompaniesController(ICompaniesService companiesService, IUserService userService, ITownService townService) : base(userService)
        {
            this.companiesService = companiesService;
            this.townService = townService;
        }

        public async Task<IActionResult> Index() =>
            View(await companiesService.GetAllCompaniesAsync());

        public async Task<IActionResult> Create()
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                await StoreTownsToViewData(townService);
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CompanyDTOin dto)
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                if (!ModelState.IsValid)
                {
                    await StoreTownsToViewData(townService);
                    return View(dto);
                }

                try
                {
                    Company result = await companiesService.CreateAsync(dto);
                    TempData["Success"] = $"Successfully was created {dto.Name} company!";
                    return RedirectToAction("Index");
                }
                catch (System.InvalidOperationException ex)
                {
                    TempData["Failure"] = ex.Message;
                    return View(dto);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var deletedCompany = await companiesService.DeleteAsync(id);
                TempData["Success"] = $"Succesfully Deleted Company {deletedCompany.Name}!";
            }
            catch (System.InvalidOperationException ex)
            {
                TempData["Failure"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                try
                {
                    var deletedAddress = await companiesService.DeleteAddressAsync(id);
                    TempData["Success"] = $"Successfully was Deleted Address at town {deletedAddress.Town.Name} from {deletedAddress.Company.Name} company!";
                }
                catch (System.InvalidOperationException ex)
                {
                    TempData["Failure"] = ex.Message;
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }
     
        //Not Implemented yet!
        public IActionResult Edit(string id)
        {
            return RedirectToAction("NotImplemented", "Home");
        }

    }
}