using DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Xml.Linq;

namespace kioskProject.Model
{
    enum SeverID
    {
        Login,
        Register,
        Pay,
        Delete, 
        Add,
        Change,
    }

    class Sever
    {
        private TcpClient tcpClient;
        private NetworkStream stream = null;
        private StreamReader strReader;


        bool check = false;

        public Sever()
        {
            tcpClient = new TcpClient();
        }



        public void PayStart(ArrayList arrayList)
        {
            if (check == false)
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            }

            check = true;

            // 리스트를 JSON 문자열로 직렬화
            string jsonData = JsonSerializer.Serialize(arrayList);

            // JSON 문자열을 바이트 배열로 변환
            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);
        }
      
        public void SeverStart(ref List<MenuInfo> list)
        {
            if (check == false)
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            }

            check = true;

            stream = tcpClient.GetStream();

            strReader = new StreamReader(tcpClient.GetStream());

            while (true)
            {
                string ad = strReader.ReadLine();

                if (ad != null)
                {
                    list = JsonSerializer.Deserialize<List<MenuInfo>>(ad);                
                    break;
                }
            }         
        }

        public bool Login(string id,string password)
        {        
            byte[] severID = Encoding.UTF8.GetBytes(SeverID.Login.ToString());
            byte[] idData = Encoding.UTF8.GetBytes(id);  // id를 바이트 배열로 변환
            byte[] passwordData = Encoding.UTF8.GetBytes(password);  // password를 바이트 배열로 변환

            List<string> strings = new List<string>();

            strings.Add(SeverID.Login.ToString());
            strings.Add(id);
            strings.Add(password);

            string jsonData = JsonSerializer.Serialize(strings);

            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);

            while (true)
            {
                string ad = strReader.ReadLine();

                if (ad != null)
                {
                    bool result = JsonSerializer.Deserialize<bool>(ad);

                    return result;                    
                }
            }

        }

        public bool Register(string id, string password)
        {               
            List<string> strings = new List<string>();

            strings.Add(SeverID.Register.ToString());
            strings.Add(id);
            strings.Add(password);

            string jsonData = JsonSerializer.Serialize(strings);

            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas,0,datas.Length);
           
            while (true)
            {
                string ad = strReader.ReadLine();

                if (ad != null)
                {
                    bool result = JsonSerializer.Deserialize<bool>(ad);

                    return result;
                }
            }

        }
        public void Delete(string Name)
        {
            List<string> strings = new List<string>();

            strings.Add(SeverID.Delete.ToString());
            strings.Add(Name);

            string jsonData = JsonSerializer.Serialize(strings);

            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);
        }
       
        public void Add(string name, int price, string image, int page)
        {
            List<string> strings = new List<string>();

            strings.Add(SeverID.Add.ToString());
            strings.Add(name);
            strings.Add(price.ToString());
            strings.Add(image);
            strings.Add(page.ToString());

            string jsonData = JsonSerializer.Serialize(strings);

            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);
        }

        public void ItemUpdate(string originalName, string name, string price, string image)
        {
            List<string> strings = new List<string>();

            strings.Add(SeverID.Change.ToString());
            strings.Add(originalName);
            strings.Add(name);
            strings.Add(price);
            strings.Add(image);

            string jsonData = JsonSerializer.Serialize(strings);

            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);

        }

    }


}
