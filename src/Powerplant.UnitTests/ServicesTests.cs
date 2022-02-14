using NSubstitute;
using NUnit.Framework;
using Powerplant.Core.Domain.Interface.Infra.Repository;
using Powerplant.Core.Domain.Model.View;
using Powerplant.Core.Service;
using Powerplant.Core.Service.Factory;
using Powerplant.Infra.Mock.Input;
using Powerplant.Infra.Mock.View;
using Powerplant.Infra.WebsocketManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powerplant.UnitTests
{
    [TestFixture]
    public class ServicesTests
    {
        private readonly ProductionPlanService _productionPlanService;
        private readonly ProductionPlanInputDTOMock _productionPlanInputMock;

        public ServicesTests()
        {
            var webSocktet = Substitute.For<IWebSocketHandler>();

            var paramRepository = Substitute.For<IParamRepository>();
            paramRepository.GetByKey(Arg.Any<string>()).Returns(Task.FromResult(new Core.Domain.Model.ParamModel { Value = "0.3" }));

            _productionPlanService = new ProductionPlanService(paramRepository, new PowerPlantFactory(), webSocktet);
            _productionPlanInputMock = new ProductionPlanInputDTOMock();
        }

        [Test]
        public void Shold_Process_ProductionPlan_WithSucess()
        {
            var actual = _productionPlanService.Process(_productionPlanInputMock.Get()).Result;
            var expected = ProductionPlanViewMock.Get();

            Assert.IsTrue(actual.ProductionPlans.SequenceEqual(expected.ProductionPlans, new PowerPlantViewComparer()));
        }

        [TestCase(1500)]
        [TestCase(2000)]
        public void Shold_Error_When_NoPowerPlanSufficient(int load)
        {
            var actual = _productionPlanService.Process(_productionPlanInputMock.Get(load)).Result;
            var expected = $"No PowerPlan for demand of power {load}";

            Assert.AreEqual(actual.Erros.First().Error, expected);
        }

        [Test]
        public void Shold_Error_When_TypeNotExist()
        {
            var powerPlanMock = _productionPlanInputMock.Generate();
            powerPlanMock.PowerPlants[0].Type = "typeteste";

            var actual = _productionPlanService.Process(powerPlanMock).Result;
            var expected = $"No PowerPlan for Type { powerPlanMock.PowerPlants[0].Type } of { powerPlanMock.PowerPlants[0].Name }";

            Assert.AreEqual(actual.Erros.Count, 1);
            Assert.AreEqual(actual.Erros.First().Error, expected);
        }
    }

    internal class PowerPlantViewComparer : IEqualityComparer<PowerPlantView>
    {
        public bool Equals(PowerPlantView x, PowerPlantView y)
        {
            if (x.Name == y.Name && x.P == y.P)
                return true;

            return false;
        }

        public int GetHashCode(PowerPlantView obj)
        {
            return obj.GetHashCode();
        }
    }

}