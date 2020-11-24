using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public class CurrencyOptionDTOout : IMapFrom<Currency>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}