using System.ComponentModel.DataAnnotations;

namespace Api.CustomActionFilters.CustomAttributes {
  public class AllowedValuesAttribute : ValidationAttribute {
    private readonly string[] _allowedValues;

    public AllowedValuesAttribute(params string[] allowedValues) {
      _allowedValues = allowedValues;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
      if (value == null ||  _allowedValues.Contains(value.ToString())) {
        return ValidationResult.Success;
      }
      else {
        return new ValidationResult($"Value is not one of the allowed values: {string.Join(", ", _allowedValues)}");
      }
    }
  }
}
