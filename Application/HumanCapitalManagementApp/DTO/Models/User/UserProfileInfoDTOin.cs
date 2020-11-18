using System;
using Helpers;
using Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using Helpers.Interfaces;

namespace DTO
{
    public class UserProfileInfoDTOin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordInputed { get; set; }
        public string PasswordHashed { get; set; }
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