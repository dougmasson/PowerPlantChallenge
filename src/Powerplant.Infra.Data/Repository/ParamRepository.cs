using Powerplant.Core.Domain.Interface.Cache;
using Powerplant.Core.Domain.Interface.Infra.Repository;
using Powerplant.Core.Domain.Model;
using Powerplant.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powerplant.Infra.Data.Repository
{
    public class ParamRepository : BaseRepository<ParamModel>, IParamRepository
    {
        private readonly ICacheService _cache;

        public ParamRepository(ApiDbContext context, ICacheService cache) : base(context)
        {
            _cache = cache;
        }

        public async Task<ParamModel> GetByKey(string key)
        {
            return await _cache.SetAsync(key, async () =>
            {
                var result = await FindByCondition(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase));
                return result.FirstOrDefault();
            });
        }

        public async override Task<IEnumerable<ParamModel>> ListAsync()
        {
            return await _cache.SetAsync("AllParams", async () =>
            {
                return await base.ListAsync();
            });
        }
    }
}
