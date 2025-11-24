using FluentValidation;
using Gateway.API.Models.Auth;
using Gateway.API.Resources;

namespace Gateway.API.Validators;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(model => model.Identifier)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Identifier)));

        RuleFor(model => model.Password)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Password)));
    }
}