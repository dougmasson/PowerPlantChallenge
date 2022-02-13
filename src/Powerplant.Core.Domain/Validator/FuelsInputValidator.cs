using FluentValidation;
using Powerplant.Core.Domain.Model.Input;

namespace Powerplant.Core.Domain.Validator
{
    public class FuelsInputValidator : AbstractValidator<FuelsInput>
    {
        public FuelsInputValidator()
        {
            RuleFor(x => x.Gas)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("gas(euro/MWh)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.Kerosine)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("kerosine(euro/MWh)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.Co2)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("co2(euro/ton)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.Wind)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("wind(%)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);
        }
    }
}
