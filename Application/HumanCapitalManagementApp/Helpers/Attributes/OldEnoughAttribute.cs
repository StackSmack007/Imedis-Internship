using System;
using System.ComponentModel.DataAnnotations;

namespace Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class OldEnoughAttribute : ValidationAttribute
    {
        private readonly int minYears;
        public OldEnoughAttribute(int minYears)
        {
            this.minYears = minYears;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (DateTime.UtcNow.Year - ((DateTime)value).Year >= minYears)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The age is less than {minYears}!");
        }
    }

}