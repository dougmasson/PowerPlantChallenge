using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Powerplant.WebsocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunWebSockets().GetAwaiter().GetResult();
        }

        private static async Task RunWebSockets()
        {
            string id = Guid.NewGuid().ToString().ToUpper();

            Console.WriteLine("==============================================");
            Console.WriteLine("|                                            |");
            Console.WriteLine("|             Starting WebSocket             |");
            Console.WriteLine($"|    {id}    |");
            Console.WriteLine("|                                            |");
            Console.WriteLine("==============================================");

            var client = new ClientWebSocket();
            await client.ConnectAsync(new Uri($"ws://localhost:8888/ws?id={ id }"), CancellationToken.None);

            Console.WriteLine();
            Console.WriteLine("***************** Connected! *****************");

            var sending = Task.Run(async () =>
            {
                string line;
                while ((line = Console.ReadLine()) != null && line != String.Empty)
                {
                    var bytes = Encoding.UTF8.GetBytes(line);
                    await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }

                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });

            var receiving = Receiving(client);

            await Task.WhenAll(sending, receiving);
        }

        private static async Task Receiving(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];

            while (true)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                    Console.WriteLine();
                }

                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }

    }
}
