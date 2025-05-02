using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public SortOption CurrentSortOption { get; set; } = SortOption.Name;
        public SortDirection CurrentSortDirection { get; set; } = SortDirection.Ascending;

        
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
    
            NotifyPropertyChanged(nameof(Products));
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                
                if (CurrentSortOption == SortOption.Name)
                {
                    filteredList = CurrentSortDirection == SortDirection.Ascending
                        ? filteredList.OrderBy(p => p?.Product?.Name)
                        : filteredList.OrderByDescending(p => p?.Product?.Name);
                }
                else if (CurrentSortOption == SortOption.Price)
                {
                    filteredList = CurrentSortDirection == SortDirection.Ascending
                        ? filteredList.OrderBy(p => p?.Product?.Price)
                        : filteredList.OrderByDescending(p => p?.Product?.Price);
                }
        
                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }
    }
}
