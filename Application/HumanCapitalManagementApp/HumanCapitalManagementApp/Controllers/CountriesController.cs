using DTO;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class CountriesController : BaseController
    {
        private readonly ICountryService countryService;

        public CountriesController(IUserService userService, ICountryService countryService) : base(userService)
        {
            this.countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {

            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                return View(await countryService.GetAllCountriesAsync());
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                CountryEDITout countryForEdit = await countryService.GetCountryForEditAsync(id);
                await StoreCountriesToTempViewData();
                return View(countryForEdit);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryEDITout dto)
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                if (!ModelState.IsValid)
                {
                    StoreCountriesToViewDataFromTempData();
                    return View(dto);
                }
                try
                {
                    await countryService.EditCountryAsync(dto);
                }
                catch (Exception ex)
                {
                    ViewData["Failure"] = ex.Message;
                    return View(dto);
                }

                TempData["Success"] = $"Country [{dto.Name}] was successfully modified!";
                return RedirectToAction("Index", "Countries");
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Add()
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CountryDTOin dto)
        {
            if (!UserService.IsLoggedIn || !UserService.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await countryService.CreateCountryAsync(dto);
            }
            catch (InvalidOperationException ex)
            {
                ViewData["Error"] = ex.Message;
                return View(dto);
            }

            TempData["Success"] = $"Country [{dto.Name}] was successfully added!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (UserService.IsLoggedIn && UserService.User.IsInRole("Admin"))
            {
                Country countryDeleted = await countryService.DeleteAsync(id);
                if (countryDeleted != null)
                {
                    TempData["Success"] = $"Country [{countryDeleted.Name}] was successfully removed!";
                }
                else
                {
                    TempData["Failure"] = $"Country [{countryDeleted.Name}] was not removed!";
                }
                return RedirectToAction("Index", "Countries");
            }

            return RedirectToAction("Index", "Home");
        }
        private async Task StoreCountriesToTempViewData()
        {
            CountryOptionDTOout[] countries = (await countryService.GetAllCountryOptionsAsync()).ToArray();
            TempData["Countries"] = JsonConvert.SerializeObject(countries);
            ViewData["Countries"] = countries;
        }
        private void StoreCountriesToViewDataFromTempData()
        {
            ViewData["Countries"] = JsonConvert.DeserializeObject<TownOptionDTOout[]>(TempData.Peek("Countries").ToString());
        }
    }
}