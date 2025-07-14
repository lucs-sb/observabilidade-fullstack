using FluentValidation;
using Gateway.API.Models.Donation;
using Gateway.API.Resources;

namespace Gateway.API.Validators;

public class DonationModelValidator : AbstractValidator<DonationModel>
{
    public DonationModelValidator()
    {
        RuleFor(model => model.DonorId)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.DonorId)));

        RuleFor(model => model.DateOfDonation).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.DateOfDonation)))
            .LessThan(DateTime.Today.AddDays(1)).WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.DonationType).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.DonationType)))
            .IsInEnum().WithMessage(ApiMessage.Gateway_Validation_Field_Fail);

        RuleFor(model => model.VolumeMl).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.VolumeMl)));

        RuleFor(model => model.BagNumber).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(model => string.Format(ApiMessage.Gateway_RequireField_Warning, nameof(model.BagNumber)));
    }
}
