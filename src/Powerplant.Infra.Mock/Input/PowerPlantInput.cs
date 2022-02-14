using Bogus;
using Powerplant.Core.Domain.Enum;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Infra.CrossCutting.ExtensionsMethods;

namespace Powerplant.Infra.Mock.Input
{
    public class PowerPlantInputMock : Faker<PowerPlantInput>
    {
        public PowerPlantInputMock()
        {
            RuleFor(o => o.Name, f => f.Lorem.Word());
            RuleFor(o => o.Type, new Randomizer().Enum<PowerPlantType>().ToDescriptionString());
            RuleFor(o => o.Efficiency, f => f.Random.Double(0.0, 0.1));
            RuleFor(o => o.Pmin, f => f.Random.Int(1, 200));
            RuleFor(o => o.Pmax, f => f.Random.Int(201, 500));
        }
    }
}
