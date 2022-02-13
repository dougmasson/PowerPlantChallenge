using Powerplant.Core.Domain.Enum;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Infra.CrossCutting;
using System;

namespace Powerplant.Core.Service.Factory
{
    /// <summary>
    /// ConcreteProduct of PowerPlant
    /// </summary>
    public class GasFired : PowerPlantProduct
    {
        public override string TypeName => PowerPlantType.GAS_FIRED.ToDescriptionString();

        public override PowerPlantProduct Create()
        {
            return new GasFired();
        }

        public override void SetValues(PowerPlantInput powerPlantInput)
        {
            base.SetValues(powerPlantInput);
            Type = PowerPlantType.GAS_FIRED;
        }

        public override void CalculatePower(FuelsInput fuels)
        {
            CostFuel = fuels.Gas.Value;
            CostCo2 = fuels.Co2.Value;
            CostGeneratePower = Math.Round(1 / Efficiency * CostFuel, 2);
        }
    }

    /// <summary>
    /// ConcreteProduct of PowerPlant
    /// </summary>
    public class TuborJet : PowerPlantProduct
    {
        public override string TypeName => PowerPlantType.TURBO_JET.ToDescriptionString();

        public override PowerPlantProduct Create()
        {
            return new TuborJet();
        }

        public override void SetValues(PowerPlantInput powerPlantInput)
        {
            base.SetValues(powerPlantInput);
            Type = PowerPlantType.TURBO_JET;
        }

        public override void CalculatePower(FuelsInput fuels)
        {
            CostFuel = fuels.Kerosine.Value;
            CostCo2 = fuels.Co2.Value;
            CostGeneratePower = Math.Round(1 / Efficiency * CostFuel, 2);
        }
    }

    /// <summary>
    /// ConcreteProduct of PowerPlant
    /// </summary>
    public class WindTurbine : PowerPlantProduct
    {
        public override string TypeName => PowerPlantType.WIND_TURBINE.ToDescriptionString();

        public override PowerPlantProduct Create()
        {
            return new WindTurbine();
        }

        public override void SetValues(PowerPlantInput powerPlantInput)
        {
            base.SetValues(powerPlantInput);
            Type = PowerPlantType.WIND_TURBINE;
        }

        public override void CalculatePower(FuelsInput fuels)
        {
            CostFuel = 0;
            CostCo2 = 0;
            CostGeneratePower = 0;
            Pmax = Math.Round(Pmax * (fuels.Wind.Value / 100));
        }

    }

}
