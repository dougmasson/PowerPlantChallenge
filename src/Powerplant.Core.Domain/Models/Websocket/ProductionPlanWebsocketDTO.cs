using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.View;
using System;
using System.Text.Json.Serialization;

namespace Powerplant.Core.Domain.Models.Websocket
{
    public class ProductionPlanWebsocketDTO
    {
        public DateTime Date { get; set; }
        public string CorellationId { get; set; }

        [JsonPropertyName("request")]
        public ProductionPlanInputDTO ProductionPlanInputDTO { get; set; }

        [JsonPropertyName("response")]
        public ProductionPlanViewDTO ProductionPlanViewDTO { get; set; }
    }
}
