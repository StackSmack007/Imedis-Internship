using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class CountryOptionDTOout : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }     
    }
}
