using System;
using System.ComponentModel.DataAnnotations;

namespace Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class SameAsAttribute : ValidationAttribute
    {
        private readonly string propertyName;

        public SameAsAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetObject = validationContext.ObjectInstance;
            var type = targetObject.GetType();
            var otherPropertyValue = type.GetProperty(propertyName).GetValue(targetObject);
            if (value.Equals(otherPropertyValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The value of {propertyName} is different!");
        }
    }

}