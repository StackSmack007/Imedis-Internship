using Helpers.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CountryEDITout : IMapFrom<Country>
    {
        public CountryEDITout()
        {
            Towns = new List<TownMoveDeleteDTOout>();
        }
        public string Id { get; set; }
        [Required,MinLength(3),MaxLength(128)]
        public string Name { get; set; }
        public List<TownMoveDeleteDTOout> Towns { get; set; }
    }
    public class TownMoveDeleteDTOout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string CountryId { get; set; }
        public bool IsDeletable { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
