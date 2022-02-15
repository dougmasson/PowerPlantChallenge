using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Powerplant.Infra.WebsocketManager
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;

        private IWebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next, IWebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            string id = GetId(context);

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await _webSocketHandler.OnConnected(socket, id);

            try
            {
                await Receive(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    }

                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocketHandler.OnDisconnected(socket);
                    }
                });
            }
            catch (Exception ex)
            {
                socket.Abort();
            }
        }

        private string GetId(HttpContext context)
        {
            string id = string.Empty;

            var queryString = context.Request.QueryString;

            if (queryString.HasValue)
            {
                Regex regex = new Regex(@"id=([a-zA-Z0-9-]+)");
                var match = regex.Match(queryString.Value);

                if (match.Success)
                {
                    id = match.Groups[1].Value;
                }
            }

            return id;
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}