using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ITownService townService;
        private const string TOWNS_OPTIONS_NAME = "Towns";

        protected BaseController(IUserService userService, ITownService townService)
        {
            UserService = userService;
            this.townService = townService;
        }
        protected IUserService UserService { get; }

        protected async Task StoreTownsToViewData()
        {
            if (!TempData.ContainsKey(TOWNS_OPTIONS_NAME))
            {
                TownOptionDTOout[] towns = (await townService.GetAllTownsAsync()).ToArray();
                TempData[TOWNS_OPTIONS_NAME] = JsonConvert.SerializeObject(towns);
            }
            ViewData[TOWNS_OPTIONS_NAME] = JsonConvert.DeserializeObject<TownOptionDTOout[]>(TempData.Peek(TOWNS_OPTIONS_NAME).ToString());
        }
        protected void ClearTownsFromTempData() =>
            TempData.Remove(TOWNS_OPTIONS_NAME);

        //protected IActionResult RedirectUnloged(string action="Index",string controller = "Home")
        //{
        //    if 
        //}
    }
}
