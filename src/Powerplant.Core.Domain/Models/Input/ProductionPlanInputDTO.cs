using System.Collections.Generic;

namespace Powerplant.Core.Domain.Model.Input
{
    public class ProductionPlanInputDTO
    {
        public double? Load { get; set; }
        public FuelsInput Fuels { get; set; }
        public List<PowerPlantInput> PowerPlants { get; set; }
    }
}