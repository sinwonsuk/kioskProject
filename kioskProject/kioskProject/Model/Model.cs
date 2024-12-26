using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kioskProject.Model
{

    class Model : INotifyPropertyChanged
    {
        private int price;
        public int Price
        {
            get { return price; }
            set { price = value;
                NotifyPropertyChanged(nameof(price));
            }
        }

        private string name;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                NotifyPropertyChanged(nameof(name));
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
