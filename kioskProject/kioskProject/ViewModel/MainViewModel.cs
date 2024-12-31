using DataBase;
using GalaSoft.MvvmLight.Command;
using kioskProject.Model;
using kioskProject.View;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace kioskProject.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Model> page1MenuModels { get; set; }
        public ObservableCollection<Model.Model> page2MenuModels { get; set; }

        public LoginModel loginModel { get; set; }
        public RegisterModel registerModel { get; set; }


        public ObservableCollection<OrderItemModel> orderItemModels { get; set; }

        public TotalPriceModel totalPrice { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        DbProject dbProject;

        public ICommand? menuClick { get; set; }
        public ICommand? plusClick { get; set; }
        public ICommand? musClick { get; set; }

        public ICommand? cancelClick { get; set; }

        public ICommand? payClick { get; set; }

        public ICommand? adminClick { get; set; }

        public ICommand? adminLoginClick { get; set; }
        public ICommand? adminRegisterClick { get; set; }

        public ICommand? adminSignUpClick { get; set; }


        TcpClient tcpClient;
        NetworkStream stream = null;

        List<MenuInfo> list = new List<MenuInfo>();


        bool check = false;
        public MainViewModel()
        {

            loginModel = new LoginModel();
            registerModel = new RegisterModel();


            // 데이터 베이스 
            dbProject = new DbProject();
            dbProject.adasdadad(list);

            // 서버 
            tcpClient = new TcpClient();



            // 이벤트 클릭 
            menuClick = new RelayCommand<object>(MenuClick);
            plusClick = new RelayCommand<object>(PlusClick);
            musClick = new RelayCommand<object>(MusClick);
            cancelClick = new RelayCommand<object>(Cancel);
            payClick = new RelayCommand<object>(Pay);
            adminClick = new RelayCommand(Admin);
            adminLoginClick = new RelayCommand<object>(AdminLoginButton);
            adminRegisterClick = new RelayCommand<object>(AdmimRegisterButton);
            adminSignUpClick = new RelayCommand<object>(AdminSignUp);
            // 모델 클래스
            totalPrice = new TotalPriceModel();
            orderItemModels = new ObservableCollection<OrderItemModel>();
            page1MenuModels = new ObservableCollection<Model.Model>();
            page2MenuModels = new ObservableCollection<Model.Model>();


            // 데이터 베이스 정보를 기반으로 화면 띄우기
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Page == "1")
                {
                    page1MenuModels.Add(new Model.Model
                    {
                        Name = list[i].Name,
                        Price = list[i].Price,
                        Image = new BitmapImage(new Uri(list[i].Image))
                    });
                }
                if (list[i].Page == "2")
                {
                    page2MenuModels.Add(new Model.Model
                    {
                        Name = list[i].Name,
                        Price = list[i].Price,
                        Image = new BitmapImage(new Uri(list[i].Image))
                    });
                }
            }
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
            if (check == false)
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            }

            check = true;

            stream = tcpClient.GetStream();

            Dictionary<string, Dictionary<string, string>> outerDict = new Dictionary<string, Dictionary<string, string>>();

            List<List<string>> list = new List<List<string>>();

            for (int i = 0; i < orderItemModels.Count; i++)
            {

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
        void Admin()
        {
            Login login = new Login();
            login.DataContext = this;
            login.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            login.ShowDialog();


        }

        void AdminLoginButton(object parameter)
        {
            string password = loginModel.Password;
            string id = loginModel.ID;

            var login = parameter as Login;


            if (dbProject.tttt(id, password) == true)
            {
                System.Windows.MessageBox.Show("로그인되었습니다");
                login.Close();

               
            }
            else
            {
                System.Windows.MessageBox.Show("잘못입력하였습니다");
                login.Close();
            }
        }
        void AdmimRegisterButton(object parameter)
        {
            Register register = new Register();
            register.DataContext = this;
            register.ShowDialog();




        }
        void AdminSignUp(object parameter)
        {

            string password = registerModel.Password;
            string id = registerModel.ID;

            var register = parameter as Register;

            if (dbProject.Adadadad(id, password) == true)
            {
                register.Close();
            }
            else
            {
                register.Close();
            }

        }
    }
}

