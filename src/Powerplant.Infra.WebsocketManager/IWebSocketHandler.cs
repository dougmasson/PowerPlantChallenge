using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Powerplant.Infra.WebsocketManager
{
    public interface IWebSocketHandler
    {
        public Task OnConnected(WebSocket socket);
        public Task OnDisconnected(WebSocket socket);
        public Task SendMessageAsync(WebSocket socket, string message);
        public Task SendMessageAsync(string socketId, string message);
        public Task SendMessageToAllAsync(string message);
        public Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}