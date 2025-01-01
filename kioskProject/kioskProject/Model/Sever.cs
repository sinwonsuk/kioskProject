using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace kioskProject.Model
{


    class Sever
    {
        private TcpClient tcpClient;
        private NetworkStream stream = null;

        bool check = false;

        public Sever()
        {
            tcpClient = new TcpClient();
        }

        public void SeverStart(Dictionary<string, Dictionary<string, string>> outerDict)
        {
            if (check == false)
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            }

            check = true;

            stream = tcpClient.GetStream();

            // 리스트를 JSON 문자열로 직렬화
            string jsonData = JsonSerializer.Serialize(outerDict);

            // JSON 문자열을 바이트 배열로 변환
            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);

        }
        

    }
}
