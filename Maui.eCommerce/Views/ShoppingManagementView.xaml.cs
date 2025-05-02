using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
    public ShoppingManagementView()
    {
        InitializeComponent();
        BindingContext = new ShoppingManagementViewModel();
        
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }

    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }

    private async void CheckoutClicked(object? sender, EventArgs e)
    {
        var viewModel = BindingContext as ShoppingManagementViewModel;
        if (viewModel != null)
        {
            string receipt = await viewModel.GenerateReceipt();
            await DisplayAlert("Receipt", receipt, "OK");
        }
    }

    private void BackToHomeClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.RefreshUX();

    }

    private void SortByNameClicked(object? sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.SortProducts(ShoppingManagementViewModel.SortOption.Name);
    }

    private void SortByPriceClicked(object? sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.SortProducts(ShoppingManagementViewModel.SortOption.Price);
    }

    private async void CreateCartClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ShoppingManagementViewModel;
        if (viewModel != null)
        {
            bool success = viewModel.CreateNewCart();
            if (!success)
            {
                await DisplayAlert("Error",
                    "Failed to create new cart. Please check if the name is valid or already exists.", "OK");
            }
        }
    }

    private async void SwitchCartClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ShoppingManagementViewModel;
        if (viewModel != null)
        {
            Console.WriteLine($"SwitchCartClicked with SelectedCartName: '{viewModel.SelectedCartName}'");
        
            if (string.IsNullOrWhiteSpace(viewModel.SelectedCartName))
            {
                await DisplayAlert("No Cart Selected", "Please select a cart from the dropdown first", "OK");
                return;
            }
        
            bool success = viewModel.SwitchCart();
        
            if (!success)
            {
                await DisplayAlert("Error", "Failed to switch cart. Please select a valid cart.", "OK");
            }
            else
            {
                await DisplayAlert("Success", $"Switched to cart: {viewModel.CurrentCartName}", "OK");
            }
        }
        else
        {
            Console.WriteLine("ViewModel is null in SwitchCartClicked!");
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Console.WriteLine("Picker_SelectedIndexChanged triggered");
        if (sender is Picker picker)
        {
        
            if (picker.SelectedItem != null)
            {
                var viewModel = BindingContext as ShoppingManagementViewModel;
                if (viewModel != null)
                {
                    Console.WriteLine($"Setting ViewModel.SelectedCartName to: {picker.SelectedItem}");
                    viewModel.SelectedCartName = picker.SelectedItem.ToString();
                }
                else
                {
                    Console.WriteLine("ViewModel is null!");
                }
            }
        }
    }

    private async void ShowCartSelectionClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ShoppingManagementViewModel;
        if (viewModel != null && viewModel.CartNames.Any())
        {
            string selection = await DisplayActionSheet("Select a Cart", "Cancel", null, 
                viewModel.CartNames.ToArray());
        
            if (!string.IsNullOrWhiteSpace(selection) && selection != "Cancel")
            {
                Console.WriteLine($"Selected cart from popup: {selection}");
                viewModel.SelectedCartName = selection;
            }
        }
        else
        {
            await DisplayAlert("No Carts", "No carts available to select", "OK");
        }
    }
}