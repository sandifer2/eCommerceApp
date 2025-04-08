namespace Library.eCommerce.DTO
{

    public class ProductDTO
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
            get { return $"{Id}. {Name}. {Price}"; }
        }

        public ProductDTO()
        {
            Name = string.Empty;
        }

        public ProductDTO(ProductDTO p)
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