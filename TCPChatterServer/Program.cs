using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static List<TcpClient> clients = new List<TcpClient>();

    static async Task Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 12387);
        listener.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            clients.Add(client);
            HandleClientAsync(client);
        }
    }

    static async void HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
                break;

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine(message);
            foreach (var c in clients)
            {
                    NetworkStream clientStream = c.GetStream();
                    await clientStream.WriteAsync(buffer, 0, bytesRead);
                
            }
        }

        client.Close();
        clients.Remove(client);
    }
}
