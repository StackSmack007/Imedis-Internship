using Helpers.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;

namespace DTO
{
    public class CompanyListItemDTOout : IMapFrom<Company>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WebPage { get; set; }
        public IList<CompanyAddressDTOout> CompanyAddresses { get; set; }

    }

    public class CompanyAddressDTOout
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool IsPrimary { get; set; }
        public string TownName { get; set; }
        public string TownCountryName { get; set; }
        public int WorkersCount { get; set; }
    }


}
