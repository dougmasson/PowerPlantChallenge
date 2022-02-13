using System.ComponentModel;

namespace Powerplant.Core.Domain.Enum
{
    public enum PowerPlantType
    {
        [Description("gasfired")]
        GAS_FIRED = 1,

        [Description("turbojet")]
        TURBO_JET = 2,

        [Description("windturbine")]
        WIND_TURBINE = 3
    }
}
