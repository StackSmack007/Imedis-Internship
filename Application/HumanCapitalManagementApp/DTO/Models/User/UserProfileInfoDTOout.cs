using System;
using System.ComponentModel.DataAnnotations;
using Helpers;
using Helpers.Interfaces;
using Infrastructure.Data;

namespace DTO
{
    public sealed class UserProfileInfoDTOout : IMapFrom<User>
    {
        [Required]
        public string Username { get; set; }       
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please provide address!")]
        public string MailingAddress { get; set; }
        [Required, OldEnough(16)]
        public DateTime DateOfBirth { get; set; }
        public int ResidenceTownId { get; set; }       
        public string ResidenceTownName { get; set; }
        public string ResidenceTownCountryName { get; set; }

        public string FullName => 
            $"{FirstName} {(string.IsNullOrEmpty(MiddleName) ? "" : MiddleName)} {LastName}";

        public string BirthDateString =>
            DateOfBirth.ToString("d MMMM, yyyy");
    }
}