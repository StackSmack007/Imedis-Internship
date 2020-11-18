using Helpers;
using Helpers.Interfaces;
using Infrastructure.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserRegisterDTOin : IMapTo<User>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [SameAs(nameof(Password))]
        public string VerifyPassword { get; set; }
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
    }
}