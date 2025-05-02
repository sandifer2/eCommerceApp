using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel => (MainViewModel)BindingContext;
        
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingManagement");
        }
        
        private void TaxRateEntry_Completed(object sender, EventArgs e)
        {
            ApplyTaxRate();
        }
        
        private void ApplyTaxRate_Clicked(object sender, EventArgs e)
        {
            ApplyTaxRate();
        }
        
        private async void ApplyTaxRate()
        {
            
            viewModel.UpdateTaxRate();
            await DisplayAlert("Tax Rate Updated", $"Tax rate has been set to {viewModel.TaxRateDisplay}", "OK");
        }
    }
}