using Helpers.Interfaces;
using Infrastructure.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class PositionDTOin : IMapTo<UserJob>
    {
        [Required(ErrorMessage ="Empoyee must be selected!")]
        public string UserId { get; set; }
        public string ManagerId { get; set; }
        [Range(1, int.MaxValue)]
        public int JobId { get; set; }
        [Range(1, int.MaxValue)]
        public int CurrencyId { get; set; }
        [Range(1, int.MaxValue)]
        public int CompanyAddressId { get; set; }      
        [Range(5, 48)]
        public int WeekHours { get; set; }
        [Range(typeof(decimal), "100", "79228162514264337593543950335")]
        public decimal MonthlySalary { get; set; }
        public DateTime StartDate { get; set; }
    }
}
