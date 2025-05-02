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

    private  async void CheckoutClicked(object? sender, EventArgs e)
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
}