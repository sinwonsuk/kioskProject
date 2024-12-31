using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kioskProject.Model
{
    class RegisterModel :INotifyPropertyChanged
    {
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
