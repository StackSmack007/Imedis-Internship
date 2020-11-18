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
            //  countryService.GetAllCountriesAsync();


            return View();
        }

        public IActionResult Privacy()
        {
            var test = new User();

            //var regData = new UserRegister
            //{
            //    Username = "test1",
            //    DateOfBirth = new DateTime(1990, 9, 6),
            //    Email = "test1@abv.bg",
            //    FirstName = "Tester",
            //    MiddleName = null,
            //    LastName = "Testov",
            //    ResidenceTownId = 2,
            //    MailingAddress = "Nepredskazuema ulica 13",
            //    Gender = Infrastructure.Data.Gender.Male,
            //    Password = "123abcd",
            //    Phone = "0883349666",
            //};
            //UserService.Register(regData);
            //UserService.Logout();
            // UserService.Login(new DTO.UserLogin {UsernameOrEmail="test1",Password= "123abcd" });

            // HttpContext a = this.HttpContext;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
