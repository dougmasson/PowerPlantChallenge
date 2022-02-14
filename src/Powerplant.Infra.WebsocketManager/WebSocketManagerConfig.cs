using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Powerplant.Infra.WebsocketManager
{
    public static class WebSocketManagerConfig
    {
        public static IServiceCollection AddWebSocketManagerServices(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            services.AddSingleton<IWebSocketHandler, WebSocketHandler>();
            return services;
        }

        public static IApplicationBuilder AddWebSocketManagerApp(this IApplicationBuilder app, PathString path)
        {
            app.UseWebSockets();

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            var handler = serviceProvider.GetService<IWebSocketHandler>();

            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }
    }
}