using Bogus;
using Powerplant.Core.Domain.Model.Input;

namespace Powerplant.Infra.Mock.Input
{
    public class FuelsInputMock : Faker<FuelsInput>
    {
        public FuelsInputMock()
        {
            RuleFor(o => o.Gas, f => f.Random.Double(2, 60));
            RuleFor(o => o.Kerosine, f => f.Random.Double(2, 60));
            RuleFor(o => o.Co2, f => f.Random.Int(1, 100));
            RuleFor(o => o.Wind, f => f.Random.Int(1, 100));
        }
    }
}
