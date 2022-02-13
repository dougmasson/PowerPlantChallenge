using Powerplant.Core.Domain.Interface;
using Powerplant.Core.Domain.Model;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.View;
using Powerplant.Core.Domain.Model.System;
using Powerplant.Core.Service.Factory;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powerplant.Core.Service
{

    /// <summary>
    /// Services of Production Plan
    /// </summary>
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly IPowerPlanFactory _powerPlanFactory;

        public ProductionPlanService(IPowerPlanFactory powerPlanFactory)
        {
            _powerPlanFactory = powerPlanFactory;
        }

        /// <summary>
        /// Process Prodution Plan
        /// </summary>
        /// <param name="productionPlanInputDTO"></param>
        /// <returns></returns>
        public Task<ProductionPlanViewDTO> Process(ProductionPlanInputDTO productionPlanInputDTO)
        {
            var productionPlanViewDTO = new ProductionPlanViewDTO();
            var powerPlants = new List<PowerPlantModel>();

            foreach (var powerPlantInput in productionPlanInputDTO.PowerPlants)
            {
                var powerPlant = _powerPlanFactory.Create(powerPlantInput, productionPlanInputDTO.Fuels);

                if (powerPlant != null)
                {
                    powerPlants.Add(powerPlant);
                }
                else
                {
                    string text = string.Format(Messages.TEXT_CODE_NO_POWER_PLAN_TYPE, powerPlantInput.Type, powerPlantInput.Name);

                    Log.Warning(text);
                    productionPlanViewDTO.Erros.Add(new ErrorDetail { Code = Messages.CODE_CODE_NO_POWER_PLAN_TYPE, Error = text });
                }
            }

            if (productionPlanViewDTO.Erros.Any())
                return Task.FromResult(productionPlanViewDTO);

            powerPlants = MeritOrder(powerPlants);

            bool result = CalculatePower(powerPlants, productionPlanInputDTO.Load.Value);

            if (result)
            {
                foreach (var item in powerPlants)
                {
                    productionPlanViewDTO.ProductionPlans.Add(new PowerPlantView { Name = item.Name, P = item.GeneratePower });
                }
            }
            else
            {
                string text = string.Format(Messages.TEXT_NO_POWER_PLAN_DEMAND_POWER, productionPlanInputDTO.Load);

                Log.Warning(text);
                productionPlanViewDTO.Erros.Add(new ErrorDetail { Code = Messages.CODE_NO_POWER_PLAN_DEMAND_POWER, Error = text });
            }

            return Task.FromResult(productionPlanViewDTO);
        }

        /// <summary>
        /// Decide to order which powerplants in the merit-order
        /// </summary>
        /// <param name="powerPlants"></param>
        /// <returns></returns>
        private List<PowerPlantModel> MeritOrder(List<PowerPlantModel> powerPlants)
        {
            return powerPlants.OrderBy(ppl => ppl.CostGeneratePower).ToList();
        }

        /// <summary>
        /// Activate Unit-commitment Problem
        /// </summary>
        /// <param name="powerPlants"></param>
        /// <param name="load"></param>
        /// <returns></returns>
        private bool CalculatePower(List<PowerPlantModel> powerPlants, double load)
        {
            double powerToGenerated = load;

            foreach (var powerPlant in powerPlants)
            {
                if (powerToGenerated >= powerPlant.Pmin)
                {
                    powerPlant.GeneratePower = powerToGenerated >= powerPlant.Pmax
                                                ? powerPlant.Pmax
                                                : powerToGenerated;

                    powerToGenerated -= powerPlant.GeneratePower;
                }
            }

            return powerPlants.Sum(x => x.GeneratePower) == load;
        }

    }
}
