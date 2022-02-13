using Powerplant.Core.Domain.Model.System;
using System.Collections.Generic;

namespace Powerplant.Core.Domain.Model.View
{
    public class ProductionPlanViewDTO
    {
        public List<ErrorDetail> Erros { get; set; } = new List<ErrorDetail>();
        public List<PowerPlantView> ProductionPlans { get; set; } = new List<PowerPlantView>();
    }
}
