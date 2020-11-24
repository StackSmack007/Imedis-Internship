using DTO;
using System;
using Services.Contracts;
using Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HumanCapitalManagementApp.Controllers
{
    public class PositionsController : BaseController
    {
        private readonly IPosititionsService positionsService;
        private readonly ICompaniesService companiesService;
        private readonly IJobService jobService;

        public PositionsController(IPosititionsService positionsService, ICompaniesService companiesService, IJobService jobService, IUserService userService) : base(userService)
        {
            this.positionsService = positionsService;
            this.companiesService = companiesService;
            this.jobService = jobService;
        }

        public async Task<IActionResult> Index(bool openOnly = true)
        {
            ICollection<PositionMiniDTOout> positions = await positionsService.GetAllMiniAsync(openOnly);
            TempData["TypeOfPositionsChosen"] = openOnly ? "Positions currently active" : "Positions that have ended";
            return View(positions);
        }

        public async Task<IActionResult> Create()
        {
            if (UserService.User.IsInRole("Admin"))
            {
                await LoadFormDataForCreateEditAsync();
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionDTOin dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataForCreateEditAsync();
                return View(dto);
            }
            try
            {
                var pos = await positionsService.CreateAsync(dto);
                FreeTempDataFromCreateEditInfo();
                TempData["Success"] = $"New Possition for user [{pos.User.Username}-{pos.User.FirstName[0]} {pos.User.LastName}] as {pos.Job.Title} was created!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Failure"] = ex.Message;
                await LoadFormDataForCreateEditAsync();
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (UserService.User.IsInRole("Admin"))
            {
                PositionEditDTOin position = await positionsService.GetPositionForEditAsync(id);
                await LoadFormDataForCreateEditAsync();
                return View(position);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PositionEditDTOin dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataForCreateEditAsync();
                return View(dto);
            }
            try
            {
                UserJob pos = await positionsService.EditAsync(dto);
                FreeTempDataFromCreateEditInfo();
                TempData["Success"] = $"Possition for user [{pos.User.Username}-{pos.User.FirstName[0]} {pos.User.LastName}] as {pos.Job.Title} was eddited!";
                bool openOnly = pos.EndDate == null;
                return RedirectToAction("Index", new { openOnly });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Failure"] = ex.Message;
                await LoadFormDataForCreateEditAsync();
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            if (UserService.User.IsInRole("Admin"))
            {
                try
                {
                    UserJob deletedPos = await positionsService.CancelAsync(id);
                    TempData["Success"] = $"Position for user {deletedPos.User.Username} as {deletedPos.Job.Title} was closed!";
                    return RedirectToAction("Index", new { openOnly = false });
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Failure"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (UserService.User.IsInRole("Admin"))
            {
                try
                {
                    UserJob deletedPos = await positionsService.DeleteAsync(id);
                    TempData["Success"] = $"Position for user {deletedPos.User.Username} as {deletedPos.Job.Title} was removed!";
                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Failure"] = ex.Message;
                    return RedirectToAction("Index", new { openOnly = false });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task LoadFormDataForCreateEditAsync()
        {
            await StoreUsersToViewData();
            await StoreJobOptions(jobService);
            await StoreCurrencyOptions(jobService);
            await StoreCompanyOfficesToViewData(companiesService);
        }
        private void FreeTempDataFromCreateEditInfo()
        {
            ClearUsersFromTempData();
            ClearJobsFromTempData();
            ClearCurrenciesFromTempData();
            ClearCompanyOfficesFromTempData();
        }
    }
}
