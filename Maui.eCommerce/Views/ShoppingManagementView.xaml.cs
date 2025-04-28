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
}