using Helpers.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class CountryDTOout : IMapFrom<Country>
    {
        public CountryDTOout()
        {
            Towns = new List<CountryTownDTOout>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<CountryTownDTOout> Towns { get; set; }

        public int UsersTotal => Towns.Sum(x => x.UsersCount);
        public int CompaniesTotal => Towns.Sum(x => x.CompanyAddressesCount);
    }
}
