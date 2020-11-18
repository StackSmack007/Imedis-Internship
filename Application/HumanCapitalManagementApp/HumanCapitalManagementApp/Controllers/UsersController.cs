using DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;
using System.Threading.Tasks;

namespace HumanCapitalManagementApp.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IUserService userService, ITownService ts) : base(userService, ts)
        { }

        public async Task<IActionResult> Register()
        {
            await StoreTownsToViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTOin dto)
        {
            if (!ModelState.IsValid)
            {
                await StoreTownsToViewData();
                return View(dto);
            }

            try
            {
                await UserService.RegisterAsync(dto);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("registration failure!", ex.Message);
            }
            ClearTownsFromTempData();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTOin dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LoginError"] = "Username or Password Invalid!";
                return View();
            }
            var attempt = await UserService.LoginAsync(dto);
            if (attempt != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["LoginError"] = "Username or Password Missmatch";
            return View(dto);
        }
               
        public async Task<IActionResult> Profile(string username) =>
                                         View(await UserService.GetProfileDataAsync(username));

        public async Task<IActionResult> EditMyProfile()
        {
            UserProfileInfoDTOin data = await UserService.GetMyProfileInfoForEditAsync();
            if (data is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await StoreTownsToViewData();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditMyProfile(UserProfileInfoDTOin dto)
        {
            if (!UserService.IsLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            bool IsPasswordCorrect = dto.PasswordHashed == dto.PasswordInputed.ToHashedString();
            if (!IsPasswordCorrect)
            {
                ModelState.AddModelError(nameof(dto.PasswordInputed), "Wrong Password!");
            }
            if (!ModelState.IsValid)
            {
                await StoreTownsToViewData();
                return View(dto);
            }

            bool result = await UserService.UpdateUserDataAsync(dto);
            return RedirectToAction("Profile", new { username = dto.Username });
        }

        public IActionResult Logout()
        {
            UserService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}