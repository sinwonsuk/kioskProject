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
using System.Windows.Media.Imaging;
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
                new Model.Model { Name = "고구마라떼", Price = 3500 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)고구마라떼.jpg")) },
                new Model.Model { Name = "곡물라떼", Price = 3000 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)곡물라떼.jpg")) },
                new Model.Model { Name = "바닐라라떼", Price = 3200 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)바닐라라떼.jpg")) },
                new Model.Model { Name = "아메리카노", Price = 2500 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)아메리카노.jpg")) },
                new Model.Model { Name = "유자차", Price = 3000 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)유자차.jpg")) },
                new Model.Model { Name = "카페라떼", Price = 2700 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)카페라떼.jpg")) },
                new Model.Model { Name = "콜드브루오리지널", Price = 3300 ,Image = new BitmapImage(new Uri("pack://application:,,,/Resource/mega_(HOT)콜드브루오리지널.jpg")) },
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
         
            Dictionary<string, Dictionary<string, string>> outerDict = new Dictionary<string, Dictionary<string, string>>();

            List<List<string>> list = new List<List<string>>();

            for (int i = 0; i < orderItemModels.Count; i++)
            {
                //list.Add(new List<string>());
                //list[i].Add(orderItemModels[i].Name);
                //list[i].Add(orderItemModels[i].Price.ToString());
                //list[i].Add(orderItemModels[i].Quantity.ToString());
                //list[i].Add(orderItemModels[i].TotalPrice.ToString());

                Dictionary<string, string> listToSend = new Dictionary<string, string>();              
                listToSend.Add("가격", orderItemModels[i].Price.ToString());
                listToSend.Add("수량", orderItemModels[i].Quantity.ToString());
                listToSend.Add("총가격", orderItemModels[i].TotalPrice.ToString());
                outerDict[orderItemModels[i].Name] = listToSend;

            }

            // 리스트를 JSON 문자열로 직렬화
            string jsonData = JsonSerializer.Serialize(outerDict);

            // JSON 문자열을 바이트 배열로 변환
            byte[] datas = Encoding.Default.GetBytes(jsonData);

            stream.Write(datas, 0, datas.Length);

            totalPrice.TotalPrice = 0;

            orderItemModels.Clear();
        }

    }
}


