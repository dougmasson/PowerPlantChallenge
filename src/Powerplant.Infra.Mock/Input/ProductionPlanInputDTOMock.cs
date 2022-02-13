using Bogus;
using Powerplant.Core.Domain.Model.Input;
using System.Collections.Generic;

namespace Powerplant.Infra.Mock.Input
{
    public class ProductionPlanInputDTOMock : Faker<ProductionPlanInputDTO>
    {
        public ProductionPlanInputDTOMock(int qtdPowerPlants = 1)
        {
            var fuelsInputMock = new FuelsInputMock();
            var powerPlantInputMock = new PowerPlantInputMock();

            RuleFor(o => o.Load, f => f.Random.Int(1, 100));
            RuleFor(o => o.Fuels, f => fuelsInputMock.Generate());
            RuleFor(o => o.PowerPlants, f => powerPlantInputMock.Generate(f.Random.Int(1, qtdPowerPlants)));
        }

        /// <summary>
        /// Get values of Payload 1 and 3
        /// </summary>
        /// <param name="load">Default Value 480</param>
        /// <returns></returns>
        public ProductionPlanInputDTO Get(double load = 480)
        {
            var fuelsInput = new FuelsInput
            {
                Gas = 13.4,
                Kerosine = 50.8,
                Co2 = 20,
                Wind = 60D
            };

            var powerPlants = new List<PowerPlantInput>
            {
                new PowerPlantInput
                {
                    Name = "gasfiredbig1",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    Pmin = 100,
                    Pmax = 460
                },
                new PowerPlantInput
                {
                    Name = "gasfiredbig2",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    Pmin = 100,
                    Pmax = 460
                },
                new PowerPlantInput
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = "gasfired",
                    Efficiency = 0.37,
                    Pmin = 40,
                    Pmax = 210
                },
                new PowerPlantInput
                {
                    Name = "tj1",
                    Type = "turbojet",
                    Efficiency = 0.3,
                    Pmin = 0,
                    Pmax = 16
                },
                new PowerPlantInput
                {
                    Name = "windpark1",
                    Type = "windturbine",
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 150
                },
                new PowerPlantInput
                {
                    Name = "windpark2",
                    Type = "windturbine",
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36
                }
            };

            return new ProductionPlanInputDTO
            {
                Load = load,
                Fuels = fuelsInput,
                PowerPlants = powerPlants
            };
        }
    
    }
}
