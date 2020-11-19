using Helpers.Interfaces;
using Infrastructure.Data;
using System;

namespace DTO
{
    public class PositionMiniDTOout : IMapFrom<UserJob>
    {
        public int Id { get; set; }
        public UserDataDTOout User { get; set; }
        public UserDataDTOout Manager { get; set; }
        public PositionJobDTOout Job { get; set; }
        public string CompanyAddressTownName { get; set; }
        public string CompanyAddressTownCountryName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressAddress { get; set; }
        //public string CompanyAddressEmail { get; set; }
        public decimal MonthlySalary { get; set; }
        public Currency Currency { get; set; }
        public int WeekHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
