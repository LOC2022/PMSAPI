using FluentValidation;
using FluentValidation.Validators;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Validators
{

    /// <summary>
    /// Add pallet details validation.
    /// </summary>
    public class AddPalletRequestValidator : AbstractValidator<PalletDetails>
    {
        public AddPalletRequestValidator()
            {
            RuleFor(request => request.PalletPartNo)
                .NotEmpty().NotNull().WithMessage("Pallet Part Number is required.")
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(request => request.KitUnit)
                .NotEmpty().NotNull()
                .GreaterThan(0)
                .WithMessage("Kit unit is required and should be greater than 0.");
        }
    }
}