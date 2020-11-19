
using DTO;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class PositionsController : BaseController
    {
        private readonly IPosititionsService positionsService;

        public PositionsController(IPosititionsService positionsService, IUserService userService, ITownService townService) : base(userService, townService)
        {
            this.positionsService = positionsService;
        }

        public async Task<IActionResult> Index(bool openOnly = true)
        {
           
            ICollection<PositionMiniDTOout> positions = await positionsService.GetPositionsAsync(openOnly);
            TempData["TypeOfPositionsChosen"] = openOnly ? "Positions currently active" : "Positions that have ended";
            return View(positions);
        }

    }
}
