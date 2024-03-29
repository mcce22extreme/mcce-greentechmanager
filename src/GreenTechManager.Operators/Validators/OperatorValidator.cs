﻿using FluentValidation;
using GreenTechManager.Core.Constants;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.Operators.Validators
{
    public class SaveOperatorValidator : AbstractValidator<SaveOperatorModel>
    {
        public SaveOperatorValidator()
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
