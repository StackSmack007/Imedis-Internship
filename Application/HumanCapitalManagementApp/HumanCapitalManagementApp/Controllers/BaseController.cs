using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading.Tasks;
using DTO;

namespace HumanCapitalManagementApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IUserService UserService { get; }
        protected UserDataDTOout User => UserService.User;
        #region Adapted
        protected BaseController(IUserService userService)
        {
            UserService = userService;
        }
        protected async Task StoreTownsToViewData(ITownService ts) =>
            await TempViewDataLoaderService.StoreTownsToViewData(TempData, ViewData, ts);
        protected void ClearTownsFromTempData() =>
            TempViewDataLoaderService.ClearTownsFromTempData(TempData);
        protected async Task StoreCompanyOfficesToViewData(ICompaniesService cs) =>
            await TempViewDataLoaderService.StoreCompanyOfficesToViewData(TempData, ViewData, cs);
        protected void ClearCompanyOfficesFromTempData() =>
            TempViewDataLoaderService.ClearCompanyOfficesFromTempData(TempData);
        protected async Task StoreUsersToViewData() =>
            await TempViewDataLoaderService.StoreUsersToViewData(TempData, ViewData, UserService);
        protected void ClearUsersFromTempData() =>
            TempViewDataLoaderService.ClearUsersFromTempData(TempData);
        protected async Task StoreJobOptions(IJobService js) =>
            await TempViewDataLoaderService.StoreJobOptions(TempData, ViewData, js);
        protected void ClearJobsFromTempData() =>
             TempViewDataLoaderService.ClearJobsFromTempData(TempData);
        protected async Task StoreCurrencyOptions(IJobService js) =>
            await TempViewDataLoaderService.StoreCurrencyOptions(TempData, ViewData, js);
        protected void ClearCurrenciesFromTempData() =>
           TempViewDataLoaderService.ClearCurrenciesFromTempData(TempData);
        #endregion
    }
}