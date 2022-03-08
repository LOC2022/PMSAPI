using  FluentValidation;
using LOC.PMS.Model;

namespace LOC.PMS.WebAPI.Validators
{

    /// <summary>
    /// 
    /// </summary>
    public class ProductRequestValidator : AbstractValidator<PalletDetailsRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductRequestValidator()
        {
            RuleFor(request => request.PalletName)
                .NotEmpty();
        }
    }
}