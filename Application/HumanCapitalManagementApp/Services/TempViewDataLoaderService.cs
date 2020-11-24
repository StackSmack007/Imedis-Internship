using DTO;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public static class TempViewDataLoaderService
    {
        private const string TOWNS_OPTIONS_NAME = "Towns";
        private const string COMPANY_OFFICES_OPTIONS_NAME = "companyOffices";
        private const string EMPLOYEE_OPTIONS = "employeeOptions";
        private const string JOB_OPTIONS = "jobOptions";
        private const string CURRENCY_OPTIONS = "currencies";
        public static async Task StoreTownsToViewData(ITempDataDictionary tempData, ViewDataDictionary viewData, ITownService ts)
        {
            if (!tempData.ContainsKey(TOWNS_OPTIONS_NAME))
            {
                TownOptionDTOout[] towns = (await ts.GetAllTownOptionsAsync()).ToArray();
                tempData[TOWNS_OPTIONS_NAME] = JsonConvert.SerializeObject(towns);
            }
            viewData[TOWNS_OPTIONS_NAME] = JsonConvert.DeserializeObject<TownOptionDTOout[]>(tempData.Peek(TOWNS_OPTIONS_NAME).ToString());
        }
        public static void ClearTownsFromTempData(ITempDataDictionary tempData) =>
            tempData.Remove(TOWNS_OPTIONS_NAME);
        public static async Task StoreCompanyOfficesToViewData(ITempDataDictionary tempData, ViewDataDictionary viewData, ICompaniesService cs)
        {
            if (!tempData.ContainsKey(COMPANY_OFFICES_OPTIONS_NAME))
            {
                CompanyOfficeOptionDTOout[] offices = (await cs.GetAllCompanyOfficeOptionsAsync()).OrderBy(x => x.Name).ToArray();
                tempData[COMPANY_OFFICES_OPTIONS_NAME] = JsonConvert.SerializeObject(offices);
            }
            viewData[COMPANY_OFFICES_OPTIONS_NAME] = JsonConvert.DeserializeObject<CompanyOfficeOptionDTOout[]>(tempData.Peek(COMPANY_OFFICES_OPTIONS_NAME).ToString());
        }
        public static void ClearCompanyOfficesFromTempData(ITempDataDictionary tempData) =>
           tempData.Remove(COMPANY_OFFICES_OPTIONS_NAME);
        public static async Task StoreUsersToViewData(ITempDataDictionary tempData, ViewDataDictionary viewData, IUserService userService)
        {
            if (!tempData.ContainsKey(EMPLOYEE_OPTIONS))
            {
                UserOptionDTOout[] users = (await userService.GetAllUsersOptionsAsync()).ToArray();
                tempData[EMPLOYEE_OPTIONS] = JsonConvert.SerializeObject(users);
            }
            viewData[EMPLOYEE_OPTIONS] = JsonConvert.DeserializeObject<UserOptionDTOout[]>(tempData.Peek(EMPLOYEE_OPTIONS).ToString());
        }
        public static void ClearUsersFromTempData(ITempDataDictionary tempData) =>
           tempData.Remove(EMPLOYEE_OPTIONS);
        public static async Task StoreJobOptions(ITempDataDictionary tempData, ViewDataDictionary viewData, IJobService js)
        {
            if (!tempData.ContainsKey(JOB_OPTIONS))
            {
                JobOptionDTOout[] jobs = (await js.GetAllJobOptionsAsync()).ToArray();
                tempData[JOB_OPTIONS] = JsonConvert.SerializeObject(jobs);
            }
            viewData[JOB_OPTIONS] = JsonConvert.DeserializeObject<JobOptionDTOout[]>(tempData.Peek(JOB_OPTIONS).ToString());
        }
        public static void ClearJobsFromTempData(ITempDataDictionary tempData) =>
           tempData.Remove(JOB_OPTIONS);
        public static async Task StoreCurrencyOptions(ITempDataDictionary tempData, ViewDataDictionary viewData, IJobService js)
        {
            if (!tempData.ContainsKey(CURRENCY_OPTIONS))
            {
                CurrencyOptionDTOout[] currencies = (await js.GetAllCurrencyOptionsAsync()).ToArray();
                tempData[CURRENCY_OPTIONS] = JsonConvert.SerializeObject(currencies);
            }
            viewData[CURRENCY_OPTIONS] = JsonConvert.DeserializeObject<Currency[]>(tempData.Peek(CURRENCY_OPTIONS).ToString());
        }
        public static void ClearCurrenciesFromTempData(ITempDataDictionary tempData) =>
            tempData.Remove(CURRENCY_OPTIONS);
    }
}
