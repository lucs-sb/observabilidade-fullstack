using FluentValidation;
using Gateway.API.Models.Donor;
using Gateway.API.Resources;

namespace Gateway.API.Validators;

public class DonorModelValidator : AbstractValidator<DonorModel>
{
    public DonorModelValidator()
    {
        RuleFor(model => model.FullName)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.FullName)));

        RuleFor(model => model.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Email)))
            .EmailAddress().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.PhoneNumber).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.PhoneNumber)))
            .Matches(@"^\d{1,14}$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.DateOfBirth).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.DateOfBirth)))
            .LessThan(DateTime.Today).WithMessage(ApiMessage.Gateway_Validation_Field_Fail)
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.Gender).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Gender)))
            .IsInEnum().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.BloodType).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.BloodType)))
            .IsInEnum().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.RhFactor).Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.RhFactor)))
           .IsInEnum().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.WeightKg).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.WeightKg)))
            .GreaterThanOrEqualTo(50m).WithMessage(ApiMessage.Gateway_Validation_Field_Fail)
            .LessThanOrEqualTo(200m).WithMessage(ApiMessage.Gateway_Validation_Field_Fail)
            .PrecisionScale(5, 2, ignoreTrailingZeros: true).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.Address).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Address)))
            .SetValidator(new AddressModelValidator()!);
    }
}
