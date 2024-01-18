using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Api.CustomActionFilters.CustomAttributes {
  public class PostcodeAttribute : ValidationAttribute{
        public override bool IsValid(object value)
        {
            var str = (string)value;
            if (str == null)
            {
                return false;
            }

            // Check if the string starts with 4 numbers and ends with 2 letters
            return Regex.IsMatch(str, @"^\d{4}.*[a-zA-Z]{2}$");
        }
    }
}
