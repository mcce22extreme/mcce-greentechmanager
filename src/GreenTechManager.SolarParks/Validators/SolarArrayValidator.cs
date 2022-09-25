using FluentValidation;
using GreenTechManager.SolarParks.Constants;
using GreenTechManager.SolarParks.Models;

namespace GreenTechManager.SolarParks.Validators
{
    public class SolarArrayValidator : AbstractValidator<SolarArrayModel>
    {
        public SolarArrayValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.PowerOutput)
                .GreaterThan(0);

            RuleFor(x => x.Size)
                .GreaterThan(0);

            RuleFor(x => x.SolarParkId)
                .GreaterThan(0);
        }
    }
}
