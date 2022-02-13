using Powerplant.Core.Domain.Model.Input;

namespace Powerplant.Core.Service.Factory
{
    /// <summary>
    /// This is an interface and declares the factory method, which returns an new object of type PowerPlan
    /// </summary>
    public interface IPowerPlanFactory
    {
        public PowerPlantProduct Create(PowerPlantInput powerPlantInput, FuelsInput fuels);
    }
}