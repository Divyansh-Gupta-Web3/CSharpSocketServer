using Fleck;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSocket
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Create Socket.IO client

            //get the socket.io server url from the command line
            int port = 3000; // Replace with your desired port number

            var server = new WebSocketServer($"ws://0.0.0.0:{port}");

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine($"Client connected: {socket.ConnectionInfo.ClientIpAddress}");
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine($"Client disconnected: {socket.ConnectionInfo.ClientIpAddress}");
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine($"Received message from client: {message}");

                    // Process the received message and emit a response
                    // For simplicity, we'll emit a hardcoded response
                    socket.Send("Hello from the server!");
                };
            });

            Console.WriteLine($"Socket.IO server is running on port {port}");
            Console.ReadLine();

            server.Dispose();
        }
    }
}