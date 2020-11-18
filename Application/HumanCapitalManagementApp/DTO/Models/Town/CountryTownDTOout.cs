using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class CountryTownDTOout : IMapFrom<Town>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UsersCount { get; set; }
        public int CompanyAddressesCount { get; set; }
    }
}
