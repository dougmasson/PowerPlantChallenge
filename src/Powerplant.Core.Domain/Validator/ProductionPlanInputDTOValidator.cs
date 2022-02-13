using FluentValidation;
using Powerplant.Core.Domain.Model.Input;

namespace Powerplant.Core.Domain.Validator
{
    public class ProductionPlanInputDTOValidator : AbstractValidator<ProductionPlanInputDTO>
    {
        public ProductionPlanInputDTOValidator()
        {
            RuleFor(x => x.Load)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);

            RuleFor(x => x.Fuels)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .SetValidator(new FuelsInputValidator());

            RuleFor(x => x.PowerPlants)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleForEach(x => x.PowerPlants)
                .SetValidator(new PowerPlantInputValidator());
        }
    }
}
