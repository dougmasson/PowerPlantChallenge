using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Model.View;
using System.Threading.Tasks;

namespace Powerplant.Core.Domain.Interface.Service
{
    public interface IProductionPlanService
    {
        public Task<ProductionPlanViewDTO> Process(ProductionPlanInputDTO productionPlanInputDTO);
    }
}
