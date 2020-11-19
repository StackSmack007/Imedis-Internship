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

        public CompaniesController(ICompaniesService companiesService, IUserService userService, ITownService townService) : base(userService, townService)
        {
            this.companiesService = companiesService;
        }

        public async Task<IActionResult> Index() =>
            View(await companiesService.GetAllCompaniesAsync());

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

        public async Task<IActionResult> Create()
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                await StoreTownsToViewData();
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
                    await StoreTownsToViewData();
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


        public IActionResult Edit(string id)
        {
            return RedirectToAction("NotImplemented", "Home");
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

    }
}
#region oldstuff
//private readonly ICountryService countryService;

//public CountriesController(IUserService userService, ICountryService countryService) : base(userService)
//{
//    this.countryService = countryService;
//}



//public async Task<IActionResult> Edit(string id)
//{
//    if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
//    {
//        CountryEDITout countryForEdit = await countryService.GetCountryForEditAsync(id);
//        await StoreCountriesToTempViewData();
//        return View(countryForEdit);
//    }

//    return RedirectToAction("Index", "Home");
//}

//[HttpPost]
//public async Task<IActionResult> Edit(CountryEDITout dto)
//{
//    if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
//    {
//        if (!ModelState.IsValid)
//        {
//            StoreCountriesToViewDataFromTempData();
//            return View(dto);
//        }
//        try
//        {
//            await countryService.EditCountryAsync(dto);
//        }
//        catch (Exception ex)
//        {
//            ViewData["Failure"] = ex.Message;
//            return View(dto);
//        }

//        TempData["Success"] = $"Country [{dto.Name}] was successfully modified!";
//        return RedirectToAction("Index", "Countries");
//    }

//    return RedirectToAction("Index", "Home");
//}


//public IActionResult Add()
//{
//    if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
//    {
//        return View();
//    }

//    return RedirectToAction("Index", "Home");
//}

//[HttpPost]
//public async Task<IActionResult> Add(CountryDTOin dto)
//{
//    if (!UserService.IsLoggedIn || !UserService.User.IsInRole("Admin"))
//    {
//        return RedirectToAction("Index", "Home");
//    }
//    if (!ModelState.IsValid)
//    {
//        return View(dto);
//    }

//    try
//    {
//        await countryService.CreateCountryAsync(dto);
//    }
//    catch (InvalidOperationException ex)
//    {
//        ViewData["Error"] = ex.Message;
//        return View(dto);
//    }

//    TempData["Success"] = $"Country [{dto.Name}] was successfully added!";
//    return RedirectToAction("Index");
//}

//[HttpPost]
//public async Task<IActionResult> DeleteCountry(string countryId)
//{
//    if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
//    {
//        Country countryDeleted = await countryService.DeleteCountryAsync(countryId);
//        if (countryDeleted != null)
//        {
//            TempData["Success"] = $"Country [{countryDeleted.Name}] was successfully removed!";
//        }
//        else
//        {
//            TempData["Failure"] = $"Country [{countryDeleted.Name}] was not removed!";
//        }
//        return RedirectToAction("Index", "Countries");
//    }

//    return RedirectToAction("Index", "Home");
//}


//private async Task StoreCountriesToTempViewData()
//{
//    CountryOptionDTOout[] countries = (await countryService.GetAllCountryOptionsAsync()).ToArray();
//    TempData["Countries"] = JsonConvert.SerializeObject(countries);
//    ViewData["Countries"] = countries;
//}
//private void StoreCountriesToViewDataFromTempData()
//{
//    ViewData["Countries"] = JsonConvert.DeserializeObject<TownOptionDTOout[]>(TempData.Peek("Countries").ToString());
//}
#endregion