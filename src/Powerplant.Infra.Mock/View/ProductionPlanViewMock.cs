using Powerplant.Core.Domain.Model.View;
using System.Collections.Generic;

namespace Powerplant.Infra.Mock.View
{
    public static class ProductionPlanViewMock
    {
        /// <summary>
        /// Get values of response for Payload 1
        /// </summary>
        /// <returns></returns>
        public static ProductionPlanViewDTO Get()
        {
            var productionPlans = new List<PowerPlantView>
            {
                new PowerPlantView
                {
                    Name = "windpark1",
                    P = 90
                },
                new PowerPlantView
                {
                    Name = "windpark2",
                    P = 22
                },new PowerPlantView
                {
                    Name = "gasfiredbig1",
                    P = 368
                },new PowerPlantView
                {
                    Name = "gasfiredbig2",
                    P = 0
                },new PowerPlantView
                {
                    Name = "gasfiredsomewhatsmaller",
                    P = 0
                },new PowerPlantView
                {
                    Name = "tj1",
                    P = 0
                },
            };

            return new ProductionPlanViewDTO
            {
                ProductionPlans = productionPlans
            };
        }

    }
}