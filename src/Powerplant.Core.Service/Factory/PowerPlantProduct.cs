using Powerplant.Core.Domain.Model;
using Powerplant.Core.Domain.Model.Input;

namespace Powerplant.Core.Service.Factory
{
    /// <summary>
    /// This defines objects the factory method creates
    /// Inheriting class PowerPlantModel, it is possible to save object in database (e.a.k. Entity Franework) without needing to convert
    /// </summary>
    public abstract class PowerPlantProduct : PowerPlantModel
    {
        public abstract string TypeName { get; }
        public abstract PowerPlantProduct Create();
        public abstract void CalculatePower(FuelsInput fuel);

        public virtual void SetValues(PowerPlantInput powerPlantInput)
        {
            Name = powerPlantInput.Name;
            Efficiency = powerPlantInput.Efficiency.Value;
            Pmin = powerPlantInput.Pmin.Value;
            Pmax = powerPlantInput.Pmax.Value;
        }
    }
}
