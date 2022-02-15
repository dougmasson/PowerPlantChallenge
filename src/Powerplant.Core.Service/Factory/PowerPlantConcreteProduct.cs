using Powerplant.Core.Domain.Enum;
using Powerplant.Core.Domain.Model;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Infra.CrossCutting.ExtensionsMethods;
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

        public override void CalculatePower(FuelsInput fuels, ParamModel paramModel)
        {
            CostFuel = fuels.Gas.Value;
            CostCo2 = fuels.Co2.Value;
            CostGeneratePower = Math.Round(1 / Efficiency * CostFuel, 2);
            CostGeneratePower += CostCo2 * Convert.ToDouble(paramModel.Value);
        }
    }

    /// <summary>
    /// ConcreteProduct of PowerPlant
    /// </summary>
    public class TurboJet : PowerPlantProduct
    {
        public override string TypeName => PowerPlantType.TURBO_JET.ToDescriptionString();

        public override PowerPlantProduct Create()
        {
            return new TurboJet();
        }

        public override void SetValues(PowerPlantInput powerPlantInput)
        {
            base.SetValues(powerPlantInput);
            Type = PowerPlantType.TURBO_JET;
        }

        public override void CalculatePower(FuelsInput fuels, ParamModel paramModel)
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

        public override void CalculatePower(FuelsInput fuels, ParamModel paramModel)
        {
            CostFuel = 0;
            CostCo2 = 0;
            CostGeneratePower = 0;
            Pmax = Math.Round(Pmax * (fuels.Wind.Value / 100));
        }

    }

}
