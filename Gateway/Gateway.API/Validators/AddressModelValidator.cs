using FluentValidation;
using Gateway.API.Models.Donor.Classes;
using Gateway.API.Resources;

namespace Gateway.API.Validators;

public class AddressModelValidator : AbstractValidator<AddressModel>
{
    public AddressModelValidator()
    {
        RuleFor(model => model.Street)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Street)));

        RuleFor(model => model.City)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.City)));

        RuleFor(model => model.State)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.State)));

        RuleFor(model => model.ZipCode)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.ZipCode)))
            .Matches(@"^\d{5}-\d{3}$").WithMessage(ApiMessage.Gateway_Validation_Field_Fail);
    }
}