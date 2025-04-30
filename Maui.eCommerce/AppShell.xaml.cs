using Maui.eCommerce.Views;

namespace Maui.eCommerce
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Product", typeof(ProductDetails));
        }
    }
}
