using Humanizer.Localisation;

namespace SageKing.Studio.Validations
{
    public class RequiredListAttribute : ValidationAttribute
    { 
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var getLengthProperty = value.GetType().GetProperty("Length");
            if (getLengthProperty != null && (int)getLengthProperty.GetValue(value) <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
