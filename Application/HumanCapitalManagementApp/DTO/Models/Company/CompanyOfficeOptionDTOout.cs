using Helpers.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;

namespace DTO
{
    public class CompanyOfficeOptionDTOout : IMapFrom<Company>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<CompanyOfficeDTOout> CompanyAddresses { get; set; } = new List<CompanyOfficeDTOout>();
    }
    public class CompanyOfficeDTOout : IMapFrom<CompanyAddress>
    {
        public int Id { get; set; }
        public bool IsPrimary { get; set; }
        public string TownName { get; set; }
        public string TownCountryName { get; set; }
    }
}