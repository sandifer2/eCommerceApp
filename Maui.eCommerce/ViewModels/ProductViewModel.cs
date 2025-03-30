using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name { 
            get
            {
                return Model?.Product?.Name ?? string.Empty;
            }

            set
            {
                if(Model != null && Model.Product?.Name != value)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public double Price {
            get
            {
                return Model?.Product?.Price ?? 0.0;
            }

            set
            {
                if(Model != null && Model.Product?.Price != value)
                {
                    Model.Product.Price = value;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public ProductViewModel() {
            Model = new Item();
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
        }
    }
}
