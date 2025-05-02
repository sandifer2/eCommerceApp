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
        
        public enum SortOption
        {
            Name,
            Price
        }

        public enum SortDirection
        {
            Ascending,
            Descending
        }
        
        public SortOption CurrentSortOption { get; set; } = SortOption.Name;
        public SortDirection CurrentSortDirection { get; set; } = SortDirection.Ascending;

        
        public Item? SelectedItem { get; set; }
        public Item? SelectedCartItem { get; set; }
        
        public ObservableCollection<Item?> Inventory
        {
            get
            {
                var items = _invSvc.Products.Where(i => i.Quantity > 0);
                
                if (CurrentSortOption == SortOption.Name)
                {
                    items = CurrentSortDirection == SortDirection.Ascending
                        ? items.OrderBy(p => p?.Product?.Name)
                        : items.OrderByDescending(p => p?.Product?.Name);
                }
                else if (CurrentSortOption == SortOption.Price)
                {
                    items = CurrentSortDirection == SortDirection.Ascending
                        ? items.OrderBy(p => p?.Product?.Price)
                        : items.OrderByDescending(p => p?.Product?.Price);
                }
        
                return new ObservableCollection<Item?>(items);
            }
        }

        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                var items = _cartSvc.CartItems.Where(i => i?.Quantity > 0);
                
                if (CurrentSortOption == SortOption.Name)
                {
                    items = CurrentSortDirection == SortDirection.Ascending
                        ? items.OrderBy(p => p?.Product?.Name)
                        : items.OrderByDescending(p => p?.Product?.Name);
                }
                else if (CurrentSortOption == SortOption.Price)
                {
                    items = CurrentSortDirection == SortDirection.Ascending
                        ? items.OrderBy(p => p?.Product?.Price)
                        : items.OrderByDescending(p => p?.Product?.Price);
                }
        
                return new ObservableCollection<Item?>(items);
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

            double taxRate = TaxRateService.Current.TaxRate; 
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
            receipt.AppendLine($"Tax ({taxRate:P0}): ${tax:F2}"); 
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
        
        public void SortProducts(SortOption option)
        {
            if (option == CurrentSortOption)
            {
                CurrentSortDirection = CurrentSortDirection == SortDirection.Ascending 
                    ? SortDirection.Descending 
                    : SortDirection.Ascending;
            }
            else
            {
                CurrentSortOption = option;
                CurrentSortDirection = SortDirection.Ascending;
            }
            
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
    }
}