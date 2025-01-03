using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using DataBase;

using System.Collections;


namespace Sever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SeverApp : Window
    {
        TcpListener chatSever;
        bool test= false;
        Socket socketClient;

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

                if (test == false)
                {
                    clientHandler.SendItems();
                    test = true;
                }

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

                byte[] buffer = new byte[2048];
                
                int check = await netStream.ReadAsync(buffer, 0, buffer.Length);

                string msg = Encoding.UTF8.GetString(buffer, 0, check);

                string[] msgs = msg.Split('\n');

                ArrayList infos = JsonSerializer.Deserialize<ArrayList>(msg);


                if (infos != null)
                {
                    string infosList = infos[0].ToString();

                    if (infosList == "Login")
                    {
                        string responseString = JsonSerializer.Serialize(project.Login(infos[1]?.ToString(), infos[2]?.ToString())); // 'true'를 직렬화하여 JSON 문자열로 변환
                        byte[] responseData = Encoding.UTF8.GetBytes(responseString + "\r\n");  // 문자열을 바이트 배열로 변환
                        netStream.Write(responseData, 0, responseData.Length);
                    }
                    else if (infosList == "Register")
                    {
                        string responseString = JsonSerializer.Serialize(project.register(infos[1]?.ToString(), infos[2]?.ToString())); // 'true'를 직렬화하여 JSON 문자열로 변환
                        byte[] responseData = Encoding.UTF8.GetBytes(responseString + "\r\n");  // 문자열을 바이트 배열로 변환
                        await netStream.WriteAsync(responseData, 0, responseData.Length);
                    }
                    else if(infosList == "Delete")
                    {
                        project.ItemDelete(infos[1]?.ToString());                   
                    }
                    else if(infosList == "Add")
                    {
                        project.SendDB(infos[1]?.ToString(),infos[2]?.ToString(), infos[3]?.ToString(), infos[4]?.ToString());
                    }
                    else if (infosList == "Change")
                    {
                        project.ItemUpdate(infos[1]?.ToString(),infos[2]?.ToString(), infos[3]?.ToString(), infos[4]?.ToString());
                    }


                    else
                    {
                        foreach (object objcet in infos)
                        {
                            string find = objcet.GetType().ToString();


                            if (objcet is JsonElement jsonElement)
                            {
                                OrderInfo orderinfo = jsonElement.Deserialize<OrderInfo>();

                                severApp.Dispatcher.Invoke(() =>
                                {
                                    severApp.textBox.Text += "이름 : "+ orderinfo.Name + "\r\n";
                                    severApp.textBox.Text += "총수량 : " +orderinfo.Quantity + "\r\n";
                                    severApp.textBox.Text += "가격 : " +orderinfo.Price + "\r\n";
                                    severApp.textBox.Text += "총가격 : " +orderinfo.TotalPrice + "\r\n";
                                });

                                project.SendOrder(orderinfo);
                            }
                        }                     
                    }
                }
                         
            }        
        }

        public bool SendItems()
        {
            try
            {
                List<MenuInfo> infos = new List<MenuInfo>();

                project.GiveFoodInfo(infos);

                string jsonString = JsonSerializer.Serialize(infos);

                byte[] bytSand_Date = Encoding.Default.GetBytes(jsonString + "\r\n");
             
                netStream.Write(bytSand_Date, 0, bytSand_Date.Length);
                return true;
            }
            catch (Exception ex)
            {             
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}