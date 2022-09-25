using FluentValidation;
using GreenTechManager.WindParks.Constants;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Validators
{
    public class WindParkValidator : AbstractValidator<WindParkModel>
    {
        public WindParkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.OperatorId)
                .GreaterThan(0);                
        }
    }
}
