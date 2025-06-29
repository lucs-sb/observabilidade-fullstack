using FluentValidation;
using Gateway.API.Models.Donor;
using Gateway.API.Resources;

namespace Gateway.API.Validators;

public class DonorModelValidator : AbstractValidator<DonorModel>
{
    private static readonly string[] AllowedGenders = { "Male", "Female" };
    private static readonly string[] AllowedBloodTypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

    public DonorModelValidator()
    {
        RuleFor(model => model.FullName)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.FullName)));

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Email)))
            .EmailAddress().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.PhoneNumber)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.PhoneNumber)))
            .Matches(@"^\d{1,14}$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.DateOfBirth)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.DateOfBirth)))
            .LessThan(DateTime.Today).WithMessage(ApiMessage.Gateway_Validation_Field_Fail)
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.Gender)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Gender)))
            .Must(gender => AllowedGenders.Contains(gender)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.BloodType)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.BloodType)))
            .Must(bloodType => AllowedBloodTypes.Contains(bloodType)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.Address)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.Address)))
            .SetValidator(new AddressModelValidator()!);
    }
}
