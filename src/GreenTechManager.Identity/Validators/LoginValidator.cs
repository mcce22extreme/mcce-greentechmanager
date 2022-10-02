using FluentValidation;
using GreenTechManager.Identity.Models;

namespace GreenTechManager.Identity.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty();

            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
