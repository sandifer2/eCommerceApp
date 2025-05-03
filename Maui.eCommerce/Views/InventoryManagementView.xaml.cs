using Library.eCommerce.Services;
using Library.eCommerce.Models;
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

    
    private async void EditClicked(object sender, EventArgs e)
    {
        var id = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
        if (!id.HasValue)
        {
            await DisplayAlert("Selection required", "Please select a product to edit.", "OK");
            return;
        }

        Console.WriteLine($"Edit product with ID: {id}");
        
        await Shell.Current.GoToAsync($"Product?productId={id}");
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
    
    private async void EditRowClicked(object sender, EventArgs e)
    {
        
        if (sender is Button button && button.CommandParameter is int productId)
        {
            Console.WriteLine($"Edit row clicked with ID: {productId}");
            await Shell.Current.GoToAsync($"Product?productId={productId}");
        }
    }

    private void DeleteRowClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int productId)
        {
            ProductServiceProxy.Current.Delete(productId);
            (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
        }
    }
    
    private async void AddToCartClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Item item)
        {
            var parent = button.Parent as Grid;
            var entry = parent?.Children.FirstOrDefault(c => c is Entry) as Entry;
    
            if (entry != null && int.TryParse(entry.Text, out int quantity) && quantity > 0)
            {   
                if (item.Quantity >= quantity)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        ShoppingCartService.Current.AddOrUpdate(item);
                    }
                
                    (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
            
                    await DisplayAlert("Added to Cart", $"{quantity} {item.Product.Name} added to cart", "OK");
                }
                else
                {
                    await DisplayAlert("Insufficient Stock", $"Only {item.Quantity} items available", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid Quantity", "Please enter a valid quantity", "OK");
            }
        }
    }

    private void SortByNameClicked(object? sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.SortProducts(InventoryManagementViewModel.SortOption.Name);
    }

    private void SortByPriceClicked(object? sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.SortProducts(InventoryManagementViewModel.SortOption.Price);

    }
}