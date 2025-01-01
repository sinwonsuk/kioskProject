using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace kioskProject.Model
{
    class AdminItemModel : INotifyPropertyChanged
    {
        private int changePrice;

        public int ChangePrice
        {
            get { return changePrice; }
            set
            {
                changePrice = value;
                NotifyPropertyChanged(nameof(changePrice));
            }
        }

        private string changeName;

        public string ChangeName
        {
            get { return changeName; }
            set
            {
                changeName = value;
                NotifyPropertyChanged(nameof(changeName));
            }
        }

        private BitmapImage changeImage;

        public BitmapImage ChangeImage
        {
            get { return changeImage; }
            set
            {
                changeImage = value;
                NotifyPropertyChanged(nameof(changeImage));
            }
        }

        private BitmapImage originalImage;
        public BitmapImage OriginalImage
        {
            get { return originalImage; }
            set
            {
                originalImage = value;
                NotifyPropertyChanged(nameof(originalImage));
            }
        }

        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private int originalPrice;

        public int OriginalPrice
        {
            get { return originalPrice; }
            set 
            { 
                originalPrice = value;
                NotifyPropertyChanged(nameof(originalPrice));
            }
        }

        private string originalName;

        public string OriginalName
        {
            get { return originalName; }
            set
            { 
                originalName = value;
                NotifyPropertyChanged(nameof(originalName));
            }
        }

        private int page;

        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
