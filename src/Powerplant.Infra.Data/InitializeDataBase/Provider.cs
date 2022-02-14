using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Powerplant.Infra.Data.Context;
using Serilog;
using System.Threading.Tasks;

namespace Powerplant.Infra.Data.InitializeData
{
    public static partial class InitializeDataExtensions
    {
        public static async Task<string> InitDataBaseAsync(this IHost host)
        {
            Log.Information("Starting Database default values");

            string provider = string.Empty;

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<ApiDbContext>())
            {
                provider = string.IsNullOrEmpty(context?.Database.ProviderName) ? ":: NONE ::" : context.Database.ProviderName;

                context.Database.EnsureCreated();
                await context.AddDataMockAsync();
            }

            Log.Information("Started Database");

            return provider;
        }
    }
}
