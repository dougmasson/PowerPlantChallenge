using Powerplant.Core.Domain.Model;
using System.Threading.Tasks;

namespace Powerplant.Core.Domain.Interface.Infra.Repository
{
    public interface IParamRepository : IRepositoryBase<ParamModel>
    {
        Task<ParamModel> GetByKey(string key);
    }
}
