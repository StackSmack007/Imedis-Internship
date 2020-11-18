using Helpers.Interfaces;
using Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CountryDTOin : IMapTo<Country>
    {
        [Required, MinLength(3), MaxLength(3)]
        public string Id { get; set; }
        [Required, MinLength(3), MaxLength(128)]
        public string Name { get; set; }
    }
}