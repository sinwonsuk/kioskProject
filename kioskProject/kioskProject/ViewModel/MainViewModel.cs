using kioskProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kioskProject.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Model> Models { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public MainViewModel()
        {
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
    }

  
}


