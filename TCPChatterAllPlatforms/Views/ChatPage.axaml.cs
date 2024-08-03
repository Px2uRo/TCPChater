using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPChatterAllPlatforms.Views;

public partial class ChatPage : UserControl

{
    public static TcpClient _client;
    private NetworkStream _stream;
    private string _host = null;
    public ChatPage()
    {
        InitializeComponent();
        TryConnectToServerAsync();
    }

    private async Task TryConnectToServerAsync()
    {
        _client = new TcpClient();
        new Thread(() => {

            while (true)
            {
                Thread.Sleep(1000);
                if (_host != null)
                {
                    _client.Connect(_host, 12387);
                    _stream = _client.GetStream();
                    StartReadingMessages();
                    break;
                }
            }
        }).Start();
    }

    private async void StartReadingMessages()
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            if (_host == null)
            {
                return;
            }
            Thread.Sleep(1000);
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Dispatcher.UIThread.Invoke(new Action(() =>
            {

            }));
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        string message = MessageInput.Text;
        byte[] data = Encoding.UTF8.GetBytes(message);
        _stream.Write(data, 0, data.Length);
        MessageInput.Text = string.Empty;
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _host = HostTB.Text;
    }

}