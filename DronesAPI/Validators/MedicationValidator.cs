using DronesAPI.Models;
using FluentValidation;

namespace DronesAPI.Validators
{
    public class MedicationValidator : AbstractValidator<Medication>
    {
        public MedicationValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id is required");
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Matches("^[a-zA-Z0-9_-]+$")
                .WithMessage("Name can only contain letters, numbers, hyphens, and underscores");
            RuleFor(m => m.Weight)
                .InclusiveBetween(0, 500)
                .WithMessage("Weight must be between 0 and 500 grams");
            RuleFor(m => m.Code)
                .NotEmpty()
                .WithMessage("Code is required")
                .Matches("^[A-Z0-9_]+$")
                .WithMessage("Code can only contain upper case letters, numbers, and underscores");
            RuleFor(m => m.Image)
                .NotEmpty()
                .WithMessage("Image is required");
        }
    }
}
