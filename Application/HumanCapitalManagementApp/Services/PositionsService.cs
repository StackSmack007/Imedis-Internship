using AutoMapper;
using DTO;
using Infrastructure.Data;
using NHibernate.Linq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PositionsService : BaseService, IPosititionsService
    {
        public PositionsService(IMapper mapper) : base(mapper)
        { }
        public async Task<ICollection<PositionMiniDTOout>> GetAllMiniAsync(bool showActive)
        {
            using (var session = NhibernateHelper.OpenSession())
            {

                ICollection<PositionMiniDTOout> result = null;
                if (showActive)
                {
                    result = (await session.Query<UserJob>().Where((x) => x.EndDate == null).Fetch(x => x.Currency).ToListAsync())
                        .Select(x => mapper.Map<PositionMiniDTOout>(x)).ToArray();
                }
                else
                {
                    result = (await session.Query<UserJob>().Where((x) => x.EndDate != null).Fetch(x => x.Currency).ToListAsync())
                        .Select(x => mapper.Map<PositionMiniDTOout>(x)).ToArray();
                }

                return result;
            }
        }
        public async Task<UserJob> CreateAsync(PositionDTOin dto)
        {
            if (dto.UserId == dto.ManagerId)
            {
                throw new InvalidOperationException("User cant be manager of himself, select none for manager");
            }

            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var newUserJob = new UserJob
                    {
                        StartDate = dto.StartDate,
                        MonthlySalary = dto.MonthlySalary,
                        WeekHours = dto.WeekHours,
                        Currency = await session.Query<Currency>().FirstOrDefaultAsync(x => x.Id == dto.CurrencyId),
                        User = await session.Query<User>().FirstOrDefaultAsync(x => x.Id == dto.UserId),
                        Manager = string.IsNullOrEmpty(dto.ManagerId) ? null
                                    : await session.Query<User>().FirstOrDefaultAsync(x => x.Id == dto.ManagerId),
                        Job = await session.Query<Job>().FirstOrDefaultAsync(x => x.Id == dto.JobId),
                        CompanyAddress = await session.Query<CompanyAddress>().FirstOrDefaultAsync(x => x.Id == dto.CompanyAddressId),
                    };
                    await session.SaveAsync(newUserJob);
                    await transaction.CommitAsync();
                    return newUserJob;
                }
            }
        }
        public async Task<PositionEditDTOin> GetPositionForEditAsync(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                UserJob uj = await session.Query<UserJob>().FirstOrDefaultAsync(x => x.Id == id);
                PositionEditDTOin posEdit = new PositionEditDTOin
                {
                    Id = uj.Id,
                    UserId = uj.User.Id,
                    ManagerId = uj.Manager?.Id,
                    JobId = uj.Job.Id,
                    CurrencyId = uj.Currency.Id,
                    CompanyAddressId = uj.CompanyAddress.Id,
                    WeekHours = uj.WeekHours,
                    MonthlySalary = uj.MonthlySalary,
                    StartDate = uj.StartDate,
                    EndDate = uj.EndDate
                };
                return posEdit;
            }
        }
        public async Task<UserJob> EditAsync(PositionEditDTOin dto)
        {
            if (dto.UserId == dto.ManagerId)
            {
                throw new InvalidOperationException("User cant be manager of himself, select none for manager");
            }

            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userJobFd = await session.Query<UserJob>().FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (userJobFd is null)
                    {
                        throw new InvalidOperationException("User not found");
                    }

                    userJobFd.StartDate = dto.StartDate;
                    userJobFd.MonthlySalary = dto.MonthlySalary;
                    userJobFd.WeekHours = dto.WeekHours;
                    userJobFd.Currency = await session.Query<Currency>().FirstOrDefaultAsync(x => x.Id == dto.CurrencyId);
                    userJobFd.User = await session.Query<User>().FirstOrDefaultAsync(x => x.Id == dto.UserId);
                    userJobFd.Manager = string.IsNullOrEmpty(dto.ManagerId) ? null
                                : await session.Query<User>().FirstOrDefaultAsync(x => x.Id == dto.ManagerId);
                    userJobFd.Job = await session.Query<Job>().FirstOrDefaultAsync(x => x.Id == dto.JobId);
                    userJobFd.CompanyAddress = await session.Query<CompanyAddress>().FirstOrDefaultAsync(x => x.Id == dto.CompanyAddressId);
                    userJobFd.EndDate = dto.EndDate;

                    await session.UpdateAsync(userJobFd);
                    await transaction.CommitAsync();
                    return userJobFd;
                }
            }
        }
        public async Task<UserJob> CancelAsync(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userJobFd = await session.Query<UserJob>()
                        .Fetch(x => x.User)
                        .Fetch(x => x.Job)
                        .FirstOrDefaultAsync(x => x.Id == id);
                    if (userJobFd is null)
                    {
                        throw new InvalidOperationException("Position was not found!");
                    }
                    if (userJobFd.EndDate != null)
                    {
                        throw new InvalidOperationException("Position has been canceled already!");
                    }

                    userJobFd.EndDate = DateTime.UtcNow.CompareTo(userJobFd.StartDate) > 0 ?
                                                DateTime.UtcNow
                                                : userJobFd.StartDate;

                    await session.UpdateAsync(userJobFd);
                    await transaction.CommitAsync();
                    return userJobFd;
                }
            }
        }
        public async Task<UserJob> DeleteAsync(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userJobFd = await session.Query<UserJob>()
                        .Fetch(x => x.User)
                        .Fetch(x => x.Job)
                        .FirstOrDefaultAsync(x => x.Id == id);
                    if (userJobFd is null)
                    {
                        throw new InvalidOperationException("Position was not found!");
                    }
                    if (userJobFd.EndDate is null)
                    {
                        throw new InvalidOperationException("Position is still active, cancel it first!");
                    }
                    await session.DeleteAsync(userJobFd);
                    await transaction.CommitAsync();
                    return userJobFd;
                }
            }
        }
    }
}


//public string UserId { get; set; }
//public string ManagerId { get; set; }
//public int JobId { get; set; }
//public int CurrencyId { get; set; }
//public int CompanyAddressId { get; set; }
//public int WeekHours { get; set; }
//public decimal MonthlySalary { get; set; }
//public DateTime StartDate { get; set; }