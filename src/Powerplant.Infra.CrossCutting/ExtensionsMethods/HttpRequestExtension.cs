using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Powerplant.Infra.CrossCutting.ExtensionsMethods
{
    public static class HttpRequestExtension
    {
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
    }
}
