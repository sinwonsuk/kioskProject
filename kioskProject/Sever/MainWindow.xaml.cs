using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;
using System.Windows.Interop;
using DataBase;
using System.Diagnostics;

namespace Sever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SeverApp : Window
    {
        TcpListener chatSever;

       

        public SeverApp()
        {
            chatSever = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);

            InitializeComponent();
            chatSever.Start();
            _ = HandleClient(chatSever);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(severOnOff.Text == "서버 종료")
            {
                chatSever.Stop();
                severOnOff.Text = "서버 시작";
            }
            else if (severOnOff.Text == "서버 시작")
            {
                chatSever.Stop();
                severOnOff.Text = "서버 종료";
            }

        }

        async Task HandleClient(TcpListener client)
        {
            while (true)
            {
                Socket socketClient;

                socketClient = await client.AcceptSocketAsync();

                ClientHandler clientHandler = new ClientHandler();
                clientHandler.ClientHandler_Setup(this, socketClient, textBox);
                _ = clientHandler.Chat_Process();
            }
        }


    }

    public class ClientHandler
    {
        private TextBox chat;
        private Socket socketClient;
        private NetworkStream netStream;
        private SeverApp severApp;
        DbProject project = new DbProject();

        public void ClientHandler_Setup(SeverApp severApp, Socket socketClient, TextBox chat)
        {
            this.chat = chat;
            this.socketClient = socketClient;
            this.netStream = new NetworkStream(socketClient);
            //severApp.clientSocketArray.Add(socketClient);
            this.severApp = severApp;
        }

        public async Task Chat_Process()
        {                                   
            while (true)
            {

              
                byte[] buffer = new byte[1024];
                
                int check = await netStream.ReadAsync(buffer, 0, buffer.Length);

                string msg = Encoding.UTF8.GetString(buffer, 0, check);

                string[] msgs = msg.Split('\n');

                if (msg != null)
                {
                    foreach (var item in msgs)
                    {
                        byte[] bytSand_Date = Encoding.Default.GetBytes(item + "\r\n");

                        string receivedJson = Encoding.Default.GetString(bytSand_Date);

                        Dictionary<string, Dictionary<string, string>> info = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(receivedJson);

                       

                        foreach (var outerKey in info)
                        {
                            severApp.Dispatcher.Invoke(() =>
                            {
                                severApp.textBox.Text += outerKey.Key+"\r\n";
                            });

                            foreach (var innerKey in outerKey.Value)
                            {
                                severApp.Dispatcher.Invoke(() =>
                                {
                                    severApp.textBox.Text += innerKey.Key ;
                                    severApp.textBox.Text += innerKey.Value;                                    
                                });                            
                            }
                            severApp.textBox.Text += "\r\n";
                        }                    
                        project.SendOrder(info);
                    }             
                }

            }


             

               
            
        }
    }
}