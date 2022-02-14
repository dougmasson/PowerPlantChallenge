using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Powerplant.Api.Middleware;
using Powerplant.Core.Domain.Interface.Service;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.System;
using Powerplant.Core.Domain.Model.View;
using Powerplant.Infra.CrossCutting.ExtensionsMethods;
using Powerplant.Infra.Mock.SwaggerExample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powerplant.API.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]")]
    public class ProductionPlanController : Controller
    {
        private readonly IProductionPlanService _productionPlanService;
        public ProductionPlanController(IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorDetail>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<PowerPlantView>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(List<ErrorDetail>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(List<ErrorDetail>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductionPlanResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> PostAsync([FromBody] ProductionPlanInputDTO productionPlanInputDTO)
        {
            productionPlanInputDTO.CorellationId = Request.GetHeader(CorrelationIdBase.KEY);

            var productionPlanViewDTO = await _productionPlanService.Process(productionPlanInputDTO);

            if (!productionPlanViewDTO.Erros.Any())
            {
                return Ok(productionPlanViewDTO.ProductionPlans);
            }
            else
            {
                if (!productionPlanViewDTO.Erros.Where(x => string.IsNullOrEmpty(x.Field)).Any())
                    return BadRequest(productionPlanViewDTO.Erros);
                else
                    return Ok(productionPlanViewDTO.Erros);
            }
        }
    }
}
