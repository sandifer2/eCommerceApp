
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;
        public List<Item> CartItems
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
            items = new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById((item.Id));
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }
            
            var existingItem = CartItems.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity++;
            }
            return existingItem;
        }
        
        public Item? ReturnItem(Item? item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }
            var itemToReturn = CartItems.FirstOrDefault(x => x.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }
            return itemToReturn;
        }



    }
}