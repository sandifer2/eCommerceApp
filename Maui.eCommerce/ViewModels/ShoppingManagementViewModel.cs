using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
            
        }
        
        public async Task<string> GenerateReceipt()
        {
            if (!ShoppingCart.Any())
            {
                return "Your cart is empty.";
            }
    
            const double taxRate = 0.07; // 7% sales tax
            double? subtotal = 0;
    
            StringBuilder receipt = new StringBuilder();
            receipt.AppendLine("=== Receipt ===");
            receipt.AppendLine();
            receipt.AppendLine("Items:");
    
            foreach (var item in ShoppingCart)
            {
                double? itemTotal = item.Product.Price * item.Quantity;
                subtotal += itemTotal;
                receipt.AppendLine($"{item.Product.Name} - ${item.Product.Price:F2} x {item.Quantity} = ${itemTotal:F2}");
            }
    
            double? tax = subtotal * taxRate;
            double? total = subtotal + tax;
    
            receipt.AppendLine();
            receipt.AppendLine($"Subtotal: ${subtotal:F2}");
            receipt.AppendLine($"Tax (7%): ${tax:F2}");
            receipt.AppendLine($"Total: ${total:F2}");
            receipt.AppendLine();
            receipt.AppendLine("Thank you for shopping with us!");
    
            return receipt.ToString();
        }
        
        
        public void ReturnItem()
        {
            if (SelectedCartItem != null)
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