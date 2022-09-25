using FluentValidation;
using GreenTechManager.Operators.Constants;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.Operators.Validators
{
    public class OperatorValidator : AbstractValidator<OperatorModel>
    {
        public OperatorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(x => x.Zip).GreaterThan(0);

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);
        }
    }
}
