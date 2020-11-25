using HumanCapitalManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStatisticsService statisticsService;

        public HomeController(ILogger<HomeController> logger, IStatisticsService statisticsService, IUserService us) : base(us)
        {
            _logger = logger;
            this.statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index() =>
            View(await statisticsService.GetStatisticsAsync());

        public IActionResult NotImplemented() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}