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

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand? StartClick { get; set; }


        public MainViewModel()
        {
            StartClick = new RelayCommand<object>(test);

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

        void test(object e)
        {
            Model.Model? ad = e as Model.Model;

            //for (int i = 0; i < orderItemModels.Count; i++)
            //{
            //    if (orderItemModels[i].Name == ad.Name)
            //    {
            //        orderItemModels[i].
            //    }
            //}


            OrderItemModel model = new OrderItemModel{Name = ad.Name, Price = ad.Price};
            orderItemModels.Add(model);
        }

    }

  
}


