using Powerplant.Core.Domain.Model.Input;
using System.Collections.Generic;
using System.Linq;

namespace Powerplant.Core.Service.Factory
{
    /// <summary>
    /// This is a class that implements the Creator class and overrides the factory method to return an instance of a ConcreteProduct.
    /// </summary>
    public class PowerPlanFactory : IPowerPlanFactory
    {
        private readonly List<PowerPlantProduct> _powerPlants;

        public PowerPlanFactory()
        {
            _powerPlants = new List<PowerPlantProduct>
            {
                new GasFired(),
                new TuborJet(),
                new WindTurbine(),
            };
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="powerPlantInput"></param>
        /// <param name="fuels"></param>
        /// <returns></returns>
        public PowerPlantProduct Create(PowerPlantInput powerPlantInput, FuelsInput fuels)
        {
            var powerPlantProduct = _powerPlants.FirstOrDefault(x => string.Equals(x.TypeName, powerPlantInput.Type, System.StringComparison.OrdinalIgnoreCase));
            
            if (powerPlantProduct != null)
            {
                var powerPlantInstance = powerPlantProduct.Create();
                powerPlantInstance.SetValues(powerPlantInput);
                powerPlantInstance.CalculatePower(fuels);

                return powerPlantInstance;
            }

            return null;
        }
    }
}
