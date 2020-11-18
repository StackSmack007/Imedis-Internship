using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CompanyDTOin
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [Required, MaxLength(64)]
        public string WebPage { get; set; }
        [Required, MaxLength(128)]
        public string Address { get; set; }
        [Required, EmailAddress]
        public virtual string Email { get; set; }
        [Required, Phone]
        public virtual string Phone { get; set; }
        public int TownId { get; set; }
    }
}