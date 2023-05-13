using DronesAPI.Models;
using FluentValidation;

namespace DronesAPI.Validators
{
    public class DroneValidator : AbstractValidator<Drone>
    {
        public DroneValidator()
        {
            RuleFor(d => d.SerialNumber).NotEmpty().MaximumLength(100);
            RuleFor(d => d.Model).NotNull()
            .Must(BeAValidModel)
            .WithMessage("Invalid Model");
            RuleFor(d => d.WeightLimit).InclusiveBetween(0, 500);
            RuleFor(d => d.BatteryCapacity).InclusiveBetween(0, 100);
            RuleFor(d => d.State).NotNull();
        }

        private bool BeAValidModel(DroneModel model)
        {
            return Enum.IsDefined(typeof(DroneModel), model);
        }
    }
}
