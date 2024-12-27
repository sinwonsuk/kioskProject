using GalaSoft.MvvmLight.Command;
using kioskProject.Model;
using kioskProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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

        public MainViewModel()
        {

            menuClick = new RelayCommand<object>(MenuClick);
            plusClick = new RelayCommand<object>(PlusClick);
            musClick = new RelayCommand<object>(MusClick);
            cancel = new RelayCommand<object>(Cancel);

            totalPrice = new TotalPriceModel();

            orderItemModels = new ObservableCollection<OrderItemModel>();

            Models = new ObservableCollection<Model.Model>
            {
                new Model.Model { Name = "test1", Price = 500 },
                new Model.Model { Name = "test2", Price = 600 },
                new Model.Model { Name = "test3", Price = 700 },
                new Model.Model { Name = "test4", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 },
                new Model.Model { Name = "test5", Price = 800 }
            };
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void MenuClick(object e)
        {
            Model.Model? ad = e as Model.Model;          
            OrderItemModel model = new OrderItemModel{Name = ad.Name, Price = ad.Price,TotalPrice = ad.Price};

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

    }

  
}


