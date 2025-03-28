using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Quantity { get; set; } = 0;

        public double Price { get; set; } = 0; 

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}. {Price}. {Quantity}";
            }
        }

        public Product()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
