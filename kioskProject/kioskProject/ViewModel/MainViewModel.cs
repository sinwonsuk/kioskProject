using DataBase;
using GalaSoft.MvvmLight.Command;
using kioskProject.Model;
using kioskProject.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace kioskProject.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Model> page1ItemModels { get; set; }
        public ObservableCollection<Model.Model> page2ItemModels { get; set; }

        public ObservableCollection<AdminItemModel> page1adminItemModels { get; set; }
        public ObservableCollection<AdminItemModel> page2adminItemModels { get; set; }


        public LoginModel loginModel { get; set; }
        public RegisterModel registerModel { get; set; }


        public ObservableCollection<OrderItemModel> orderItemModels { get; set; }

        public TotalPriceModel totalPrice { get; set; }

        public AdminItemModel adminItemModel { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        DbProject dbProject;

        Sever sever;

        public ICommand? menuClick { get; set; }
        public ICommand? plusClick { get; set; }
        public ICommand? musClick { get; set; }
        public ICommand? cancelClick { get; set; }
        public ICommand? payClick { get; set; }
        public ICommand? adminClick { get; set; }
        public ICommand? adminLoginClick { get; set; }
        public ICommand? adminRegisterClick { get; set; }
        public ICommand? adminSignUpClick { get; set; }

        public ICommand? adminImageChangeClick { get; set; }

        public ICommand? adminItemDeleteClick { get; set; }
        public ICommand? adminItemChangeClick { get; set; }

        public ICommand? adminItemAddClick { get; set; }

       

        List<MenuInfo> list = new List<MenuInfo>();

        bool check = false;
        public MainViewModel()
        {

            loginModel = new LoginModel();
            registerModel = new RegisterModel();


            // 데이터 베이스 
            dbProject = new DbProject();
            dbProject.GiveFoodInfo(list);

            // 서버 
            sever = new Sever();



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
            adminImageChangeClick = new RelayCommand<object>(AdminImageChange);
            adminItemDeleteClick = new RelayCommand<object>(AdminDeleteItem);
            adminItemChangeClick = new RelayCommand<object>(AdminChangeItem);
            adminItemAddClick = new RelayCommand<object>(AdminAddItem);
            // 모델 클래스
            totalPrice = new TotalPriceModel();
            adminItemModel = new AdminItemModel();
            orderItemModels = new ObservableCollection<OrderItemModel>();
            page1ItemModels = new ObservableCollection<Model.Model>();
            page2ItemModels = new ObservableCollection<Model.Model>();
            page1adminItemModels = new ObservableCollection<AdminItemModel>();
            page2adminItemModels = new ObservableCollection<AdminItemModel>();
            // 데이터 베이스 정보를 기반으로 화면 띄우기
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Page == "1")
                {
                    page1ItemModels.Add(new Model.Model
                    {
                        Name = list[i].Name,
                        Price = list[i].Price,
                        Image = new BitmapImage(new Uri(list[i].Image))
                    });

                    page1adminItemModels.Add(new AdminItemModel
                    {
                        OriginalName = list[i].Name,
                        OriginalPrice = list[i].Price,
                        OriginalImage = new BitmapImage(new Uri(list[i].Image)),
                        Page = 1

                    });

                }
                if (list[i].Page == "2")
                {
                    page2ItemModels.Add(new Model.Model
                    {
                        Name = list[i].Name,
                        Price = list[i].Price,
                        Image = new BitmapImage(new Uri(list[i].Image))
                    });

                    page2adminItemModels.Add(new AdminItemModel
                    {
                        OriginalName = list[i].Name,
                        OriginalPrice = list[i].Price,
                        OriginalImage = new BitmapImage(new Uri(list[i].Image)),
                        Page = 2
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
            Model.Model? menu = e as Model.Model;


            for (int j = 0; j < orderItemModels.Count; j++)
            {
                if (menu.Name == orderItemModels[j].Name)
                {
                    orderItemModels[j].Quantity += 1;
                    orderItemModels[j].TotalPrice += orderItemModels[j].Price;
                    totalPrice.TotalPrice += orderItemModels[j].Price;
                    return;
                }
            }

            OrderItemModel model = new OrderItemModel { Name = menu.Name, Price = menu.Price, TotalPrice = menu.Price };
            orderItemModels.Add(model);
            totalPrice.TotalPrice += menu.Price;
        }
        void PlusClick(object e)
        {
            OrderItemModel? orderitemModel = e as OrderItemModel;

            orderitemModel.Quantity += 1;
            orderitemModel.TotalPrice += orderitemModel.Price;
            totalPrice.TotalPrice += orderitemModel.Price;
        }

        void MusClick(object e)
        {
            OrderItemModel? orderitemModel = e as OrderItemModel;

            if (orderitemModel.Quantity == 1)
            {
                return;
            }

            orderitemModel.Quantity -= 1;
            orderitemModel.TotalPrice -= orderitemModel.Price;
            totalPrice.TotalPrice -= orderitemModel.Price;
        }

        void Cancel(object e)
        {
            OrderItemModel? orderitemModel = e as OrderItemModel;

            orderItemModels.Remove(orderitemModel);

            totalPrice.TotalPrice -= orderitemModel.TotalPrice;
        }
        void Pay(object e)
        {
            //if (check == false)
            //{
            //    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            //}

            //check = true;

            //stream = tcpClient.GetStream();

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

            sever.SeverStart(outerDict);


            // 리스트를 JSON 문자열로 직렬화
            //string jsonData = JsonSerializer.Serialize(outerDict);

            //// JSON 문자열을 바이트 배열로 변환
            //byte[] datas = Encoding.Default.GetBytes(jsonData);

            //stream.Write(datas, 0, datas.Length);

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


            if (dbProject.Login(id, password) == true)
            {
                System.Windows.MessageBox.Show("로그인되었습니다");
                login.Close();

                AdminWindow adminItem = new AdminWindow();
                adminItem.DataContext = this;
                adminItem.ShowDialog();

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
            if (dbProject.register(id, password) == true)
            {
                register.Close();
            }
           
        }

        void AdminImageChange(object parameter)
        {
            var adminItemModel = parameter as AdminItemModel;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                adminItemModel.ChangeImage = new BitmapImage(new Uri(path));

                adminItemModel.ImagePath = path;
            }
        }

        void AdminDeleteItem(object parameter)
        {           
            if(DeleteProcess(parameter, page1ItemModels,page1adminItemModels)==true)
            {
                return;
            }
            if (DeleteProcess(parameter, page2ItemModels, page2adminItemModels) == true)
            {
                return;
            }         
        }     
        void AdminChangeItem(object parameter)
        {
            if(AdminChangeProcess(parameter,page1ItemModels)==true)
            {
                return;
            }
            if (AdminChangeProcess(parameter, page2ItemModels) == true)
            {
                return;
            }          
        }
        void AdminAddItem(object parameter)
        {
            if (AdminAddProcess(parameter, page1ItemModels, page1adminItemModels) == true)
            {
                return;
            }
            if (AdminAddProcess(parameter, page2ItemModels, page2adminItemModels) == true)
            {
                return;
            }
        }
        bool AdminAddProcess(object parameter, ObservableCollection<Model.Model> itemModels, ObservableCollection<AdminItemModel> adminItemModels)
        {
            var adminItemModel = parameter as AdminItemModel;

            for (int i = 0; i < itemModels.Count; i++)
            {
                if (itemModels[i].Name == adminItemModel.OriginalName)
                {
                    dbProject.SendDB(adminItemModel.ChangeName, adminItemModel.ChangePrice, adminItemModel.ImagePath, adminItemModel.Page);

                    itemModels.Add(new Model.Model
                    {
                        Name = adminItemModel.ChangeName,
                        Price = adminItemModel.ChangePrice,
                        Image = adminItemModel.ChangeImage
                    });
                    adminItemModels.Add(new AdminItemModel
                    {
                        OriginalName = adminItemModel.ChangeName,
                        OriginalPrice = adminItemModel.ChangePrice,
                        OriginalImage = adminItemModel.ChangeImage
                    });

                    System.Windows.MessageBox.Show("제품이 추가되었습니다");

                    adminItemModel.ChangeImage = null;
                    adminItemModel.ChangePrice = 0;
                    adminItemModel.ChangeName = "";

                    return true;
                }
            }
            return false;
        }    
        bool DeleteProcess(object parameter, ObservableCollection<Model.Model> itemModels, ObservableCollection<AdminItemModel> adminItemModels)
        {
            var adminItemModel = parameter as AdminItemModel;

            for (int i = 0; i < itemModels.Count; i++)
            {
                if (itemModels[i].Name == adminItemModel.OriginalName)
                {
                    itemModels.Remove(itemModels[i]);
                    adminItemModels.Remove(adminItemModel);
                    dbProject.ItemDelete(adminItemModel.OriginalName);

                    System.Windows.MessageBox.Show("제품이 삭제되었습니다");
                    return true;
                }
            }
            return false;
        }

        bool AdminChangeProcess(object parameter, ObservableCollection<Model.Model> itemModels)
        {
            var adminItemModel = parameter as AdminItemModel;

            for (int i = 0; i < itemModels.Count; i++)
            {
                if (itemModels[i].Name == adminItemModel.OriginalName)
                {
                    dbProject.ItemUpdate(adminItemModel.OriginalName, adminItemModel.ChangeName, adminItemModel.ChangePrice, adminItemModel.ImagePath);

                    itemModels[i].Price = adminItemModel.ChangePrice;
                    itemModels[i].Name = adminItemModel.ChangeName;
                    itemModels[i].Image = adminItemModel.ChangeImage;

                    adminItemModel.OriginalImage = adminItemModel.ChangeImage;
                    adminItemModel.OriginalPrice = adminItemModel.ChangePrice;
                    adminItemModel.OriginalName = adminItemModel.ChangeName;

                    adminItemModel.ChangeImage = null;
                    adminItemModel.ChangePrice = 0;
                    adminItemModel.ChangeName = "";

                    System.Windows.MessageBox.Show("제품이 변경되었습니다");
                    return true;
                }
            }
            return false;
        }       
    }
}

