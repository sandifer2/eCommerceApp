namespace Library.eCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        

        public double Price { get; set; } = 0; 

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
    }
}
