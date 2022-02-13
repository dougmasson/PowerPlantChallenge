using FluentValidation;
using Powerplant.Core.Domain.Model.Input;
using System.Collections.Generic;

namespace Powerplant.Core.Domain.Validator
{
    public class PowerPlantInputValidator : AbstractValidator<PowerPlantInput>
    {
        public PowerPlantInputValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleFor(x => x.Type)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleFor(x => x.Efficiency)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);

            RuleFor(x => x.Pmin)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.Pmax)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);
        }
    }
}
