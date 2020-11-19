using HumanCapitalManagementApp.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System.Diagnostics;

namespace HumanCapitalManagementApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountryService countryService;

        public HomeController(ILogger<HomeController> logger, ICountryService countryService, IUserService us, ITownService ts) : base(us, ts)
        {
            _logger = logger;
            this.countryService = countryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotImplemented()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var test = new User();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
