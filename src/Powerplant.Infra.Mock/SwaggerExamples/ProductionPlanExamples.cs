using Powerplant.Core;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.System;
using Powerplant.Core.Domain.Model.View;
using Powerplant.Infra.Mock.Input;
using Powerplant.Infra.Mock.View;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;

namespace Powerplant.Infra.Mock.SwaggerExample
{
    public class InternalServerErrorExample : IExamplesProvider<List<ErrorDetail>>
    {
        public List<ErrorDetail> GetExamples()
        {
            return new List<ErrorDetail>
            {
                new ErrorDetail { Code = "500", Error = HttpStatusCode.InternalServerError.ToString() }
            };
        }
    }

    public class BadRequestExample : IExamplesProvider<List<ErrorDetail>>
    {
        public List<ErrorDetail> GetExamples()
        {
            return new List<ErrorDetail>
            {
                new ErrorDetail { Field = "Load", Error = Messages.FIELD_GREATER_ZERO },
                new ErrorDetail { Field = "Fuels.gas(euro/MWh)", Error = Messages.FIELD_REQUIRED }
            };
        }
    }

    public class ProductionPlanRequesExample : IExamplesProvider<ProductionPlanInputDTO>
    {
        public ProductionPlanInputDTO GetExamples()
        {
            var productionPlanInputDTOMock = new ProductionPlanInputDTOMock();
            return productionPlanInputDTOMock.Get();
        }
    }

    public class ProductionPlanResponseExample : IExamplesProvider<List<PowerPlantView>>
    {
        public List<PowerPlantView> GetExamples()
        {
            return ProductionPlanViewMock.Get().ProductionPlans;
        }
    }
}
