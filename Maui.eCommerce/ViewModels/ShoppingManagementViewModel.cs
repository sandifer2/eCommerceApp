using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        
        public Item? SelectedItem { get; set; }
        
        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products);
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
               var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);
               if (updatedItem != null && updatedItem.Quantity > 0)
               {
                   NotifyPropertyChanged(nameof(Inventory));
               }
               

            }

        }
    }
}