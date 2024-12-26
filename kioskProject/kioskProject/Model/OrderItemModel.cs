using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kioskProject.Model
{
    internal class OrderItemModel : INotifyPropertyChanged
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

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value;
                  NotifyPropertyChanged(nameof(quantity));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
