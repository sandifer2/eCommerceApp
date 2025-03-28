using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void EditClicked(object sender, EventArgs e)
    {//TODO: ?????????????
        var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
        if (productId == null)
        {
            DisplayAlert("Selection required", "Please select a product to edit.", "OK");
            return;
        }
        
        Shell.Current.GoToAsync($"//Product?productId={productId}");
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
    
    private void EditRowClicked(object sender, EventArgs e)
    {
        // Get the product ID from the button's CommandParameter
        if (sender is Button button && button.CommandParameter is int productId)
        {
            Shell.Current.GoToAsync($"//Product?productId={productId}");
        }
    }

    private void DeleteRowClicked(object sender, EventArgs e)
    {
        // Get the product ID from the button's CommandParameter
        if (sender is Button button && button.CommandParameter is int productId)
        {
            // Delete the product directly
            ProductServiceProxy.Current.Delete(productId);
        
            // Refresh the list after deletion
            (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
        }
    }
}