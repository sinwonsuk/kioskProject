using GalaSoft.MvvmLight.Command;
using kioskProject.Model;
using kioskProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Tar;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

namespace kioskProject.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Model> Models { get; set; }
        public ObservableCollection<OrderItemModel> orderItemModels { get; set; }

        public TotalPriceModel totalPrice { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand? menuClick { get; set; }
        public ICommand? plusClick { get; set; }
        public ICommand? musClick { get; set; }

        public ICommand? cancel { get; set; }

        public ICommand? pay { get; set; }

        TcpClient tcpClient;
        NetworkStream stream = null;
        bool check = false;
        public MainViewModel()
        {
            tcpClient = new TcpClient();

            menuClick = new RelayCommand<object>(MenuClick);
            plusClick = new RelayCommand<object>(PlusClick);
            musClick = new RelayCommand<object>(MusClick);
            cancel = new RelayCommand<object>(Cancel);
            pay = new RelayCommand<object>(Pay);


            totalPrice = new TotalPriceModel();

            orderItemModels = new ObservableCollection<OrderItemModel>();

            Models = new ObservableCollection<Model.Model>
            {
                new Model.Model { Name = "test1", Price = 500 },
                new Model.Model { Name = "test2", Price = 600 },
                new Model.Model { Name = "test3", Price = 700 },
                new Model.Model { Name = "test4", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test6", Price = 800 },
                new Model.Model { Name = "test7", Price = 800 },
                new Model.Model { Name = "test8", Price = 800 },
                new Model.Model { Name = "test9", Price = 800 },
                new Model.Model { Name = "test10", Price = 800 }
            };
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        void MenuClick(object e)
        {
            Model.Model? ad = e as Model.Model;


            for (int j = 0; j < orderItemModels.Count; j++)
            {
                if (ad.Name == orderItemModels[j].Name)
                {
                    orderItemModels[j].Quantity += 1;
                    orderItemModels[j].TotalPrice += orderItemModels[j].Price;
                    totalPrice.TotalPrice += orderItemModels[j].Price;
                    return;
                }
            }

            OrderItemModel model = new OrderItemModel { Name = ad.Name, Price = ad.Price, TotalPrice = ad.Price };
            orderItemModels.Add(model);
            totalPrice.TotalPrice += ad.Price;
        }
        void PlusClick(object e)
        {
            OrderItemModel? ad = e as OrderItemModel;

            ad.Quantity += 1;
            ad.TotalPrice += ad.Price;
            totalPrice.TotalPrice += ad.Price;
        }

        void MusClick(object e)
        {
            OrderItemModel? ad = e as OrderItemModel;

            if (ad.Quantity == 1)
            {
                return;
            }

            ad.Quantity -= 1;
            ad.TotalPrice -= ad.Price;
            totalPrice.TotalPrice -= ad.Price;
        }

        void Cancel(object e)
        {
            OrderItemModel? ad = e as OrderItemModel;

            orderItemModels.Remove(ad);

            totalPrice.TotalPrice -= ad.TotalPrice;
        }
        void Pay(object e)
        {
            if(check == false)
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            }
            check = true;
           
            stream = tcpClient.GetStream();
         
            string dateToSend = "잘갑니다" + "\r\n";
         
            List<string> listToSend = new List<string> { "안녕하세요", "잘갑니다", "또 만나요" };

            // 리스트를 JSON 문자열로 직렬화
            string jsonData = JsonSerializer.Serialize(listToSend);

            // JSON 문자열을 바이트 배열로 변환
            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);

            totalPrice.TotalPrice = 0;
        }

    }
}


