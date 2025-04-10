using Library.eCommerce.DTO;

namespace Library.eCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        
        public double Price { get; set; } = 0; 
        
        public string LegacyProperty1 { get; set; }
        public string LegacyProperty2 { get; set; }
        public string LegacyProperty3 { get; set; }
        public string LegacyProperty4 { get; set; }
        public string LegacyProperty5 { get; set; }
        public string LegacyProperty6 { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}. {Price}";
            }
        }

        public Product()
        {
            Name = string.Empty;
        }

        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            Price = p.Price;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        
        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            Price = p.Price;
            LegacyProperty1 = string.Empty;
            LegacyProperty2 = string.Empty;
            LegacyProperty3 = string.Empty;
            LegacyProperty4 = string.Empty;
            LegacyProperty5 = string.Empty;
            LegacyProperty6 = string.Empty;

        }
    }
}
