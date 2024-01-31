using RPGGame.Infrastructure.Constants;
using RPGGame.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace RPGGame.Infrastructure.CustomValidationAttributes
{
    public class PlayerNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value?.ToString();

            if (name != nameof(Warrior)
                && name != nameof(Archer)
                && name != nameof(Mage))
            {
                return new ValidationResult(InfrastructureConstants.Player.PLAYER_NAME_ERROR_MESSAGE);
            }

            return ValidationResult.Success;
        }
    }
}
