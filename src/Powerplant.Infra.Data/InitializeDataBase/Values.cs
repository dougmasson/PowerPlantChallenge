using Microsoft.EntityFrameworkCore;
using Powerplant.Core.Domain.Model;
using Powerplant.Infra.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Powerplant.Infra.Data.InitializeData
{
    public static partial class InitializeDataExtensions
    {
        public static async Task AddDataMockAsync(this ApiDbContext dbContext)
        {
            var paramTable = dbContext.Set<ParamModel>();

            if (!await paramTable.AnyAsync())
            {
                List<ParamModel> lst = new List<ParamModel>()
                {
                    new ParamModel { Key = "Co2Ton", Value = "0.3" },
                };

                paramTable.AddRange(lst);
            }

            dbContext.SaveChanges();
        }
    }
}
