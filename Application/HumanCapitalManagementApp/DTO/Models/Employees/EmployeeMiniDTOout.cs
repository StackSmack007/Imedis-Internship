using Helpers.Interfaces;
using System;
using Infrastructure.Data;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DTO
{
    //Mapping configured manually in CFG [User - > EmployeeMiniDTOout]
    public class EmployeeMiniDTOout : IEqualityComparer<EmployeeMiniDTOout>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public virtual string MailingAddress { get; set; }
        public string ResidenceTownName { get; set; }
        public string ResidenceTownCountryName { get; set; }
        public virtual IList<EmployeeJobDTOout> UserJobs { get; set; } = new List<EmployeeJobDTOout>();
        public ICollection<string> ManagedUsers { get; set; } = new List<string>();
        public decimal TotalIncome => UserJobs.Where(x => x.JobActive).Sum(x => x.CurrencyRate * x.MonthlySalary);
        public int TotalWorkHoursPerMonth => UserJobs.Where(x => x.JobActive).Sum(x => x.WeekHours * 4);
        public bool Equals(EmployeeMiniDTOout x, EmployeeMiniDTOout y) => x.Username == y.Username;
        public int GetHashCode([DisallowNull] EmployeeMiniDTOout obj) => obj.Username.GetHashCode();
    }

    public class EmployeeJobDTOout : IMapFrom<UserJob>
    {
        public int Id { get; set; }
        public string ManagerUserName { get; set; }
        public string JobTitle { get; set; }
        public string JobDepartmentTitle { get; set; }
        public string CompanyAddressCompanyName { get; set; }
        public string CompanyAddressAddress { get; set; }
        public string CompanyAddressTownName { get; set; }
        public string CompanyAddressTownCountryName { get; set; }
        public decimal MonthlySalary { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyRate { get; set; }
        public int WeekHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool JobActive => DateTime.MinValue.Equals(EndDate);
    }
}
