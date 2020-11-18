using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class TownOptionDTOout : IMapFrom<Town>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
    }
}
