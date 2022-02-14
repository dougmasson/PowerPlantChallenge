using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Powerplant.Core.Domain.Model.Input
{
    public class ProductionPlanInputDTO
    {
        public double? Load { get; set; }
        public FuelsInput Fuels { get; set; }
        public List<PowerPlantInput> PowerPlants { get; set; }

        [JsonIgnore]
        public string CorellationId { get; set; }
    }
}