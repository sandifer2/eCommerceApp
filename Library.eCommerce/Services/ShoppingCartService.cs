
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private List<Product> items;
        private List<Product> CartItems
        {
            get
            {
                return items;
            }
        }
        public static ShoppingCartService Current 
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            }
        }

        private static ShoppingCartService? instance;

        private ShoppingCartService() {
            items = new List<Product>();
        }



    }
}