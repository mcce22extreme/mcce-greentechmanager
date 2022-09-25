using FluentValidation;
using GreenTechManager.SolarParks.Constants;
using GreenTechManager.SolarParks.Models;

namespace GreenTechManager.SolarParks.Validators
{
    public class SolarParkValidator : AbstractValidator<SolarParkModel>
    {
        public SolarParkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.OperatorId)
                .GreaterThanOrEqualTo(0);
        }
    }
}
