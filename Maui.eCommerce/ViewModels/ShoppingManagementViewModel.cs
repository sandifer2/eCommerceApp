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
        public Item? SelectedCartItem { get; set; }
        
        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products.Where(i => i.Quantity > 0));
            }
        }

        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<Item?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0));
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
                var shouldRefresh = SelectedItem.Quantity >= 1;
               var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);
               
               if (updatedItem != null && shouldRefresh)
               {
                   NotifyPropertyChanged(nameof(Inventory));
                   NotifyPropertyChanged(nameof(ShoppingCart));
               }
            }
        }

        public void ReturnItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
    }
}