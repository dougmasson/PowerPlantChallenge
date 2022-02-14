using Powerplant.Core.Domain.Interface.Infra.Repository;
using Powerplant.Core.Domain.Interface.Service;
using Powerplant.Core.Domain.Model;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.System;
using Powerplant.Core.Domain.Model.View;
using Powerplant.Core.Domain.Models.Websocket;
using Powerplant.Core.Service.Factory;
using Powerplant.Infra.WebsocketManager;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Powerplant.Core.Service
{

    /// <summary>
    /// Services of Production Plan
    /// </summary>
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly IPowerPlantFactory _powerPlanFactory;
        private readonly IWebSocketHandler _webSocketHandler;
        private readonly IParamRepository _paramRepository;

        public ProductionPlanService(IParamRepository paramRepository, IPowerPlantFactory powerPlanFactory, IWebSocketHandler webSocketHandler)
        {
            _powerPlanFactory = powerPlanFactory;
            _webSocketHandler = webSocketHandler;
            _paramRepository = paramRepository;
        }

        /// <summary>
        /// Process Prodution Plan
        /// </summary>
        /// <param name="productionPlanInputDTO"></param>
        /// <returns></returns>
        public async Task<ProductionPlanViewDTO> Process(ProductionPlanInputDTO productionPlanInputDTO)
        {
            var productionPlanViewDTO = new ProductionPlanViewDTO();
            var powerPlants = new List<PowerPlantModel>();

            var paramCoTon = await _paramRepository.GetByKey("Co2Ton");

            foreach (var powerPlantInput in productionPlanInputDTO.PowerPlants)
            {
                var powerPlant = _powerPlanFactory.Create(powerPlantInput, productionPlanInputDTO.Fuels, paramCoTon);

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
            {
                return await Task.FromResult(productionPlanViewDTO);
            }

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

            await SendMessageWebsocket(productionPlanInputDTO, productionPlanViewDTO);

            return await Task.FromResult(productionPlanViewDTO);
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

        /// <summary>
        /// Send Message on Websocket
        /// </summary>
        /// <param name="productionPlanInputDTO"></param>
        /// <param name="productionPlanViewDTO"></param>
        /// <returns></returns>
        private async Task SendMessageWebsocket(ProductionPlanInputDTO productionPlanInputDTO, ProductionPlanViewDTO productionPlanViewDTO)
        {
            var productionPlanWebsocketDTO = new ProductionPlanWebsocketDTO
            {
                Date = DateTime.Now,
                CorellationId = productionPlanInputDTO.CorellationId,
                ProductionPlanInputDTO = productionPlanInputDTO,
                ProductionPlanViewDTO = productionPlanViewDTO,
            };

            string message = JsonSerializer.Serialize(productionPlanWebsocketDTO, new JsonSerializerOptions
                                                       { 
                                                          DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                                                          WriteIndented = true
                                                       });

            await _webSocketHandler.SendMessageToAllAsync(message);
            Log.Information("Send message websocket");
        }
    
    }
}
