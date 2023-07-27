namespace GroceryAPi.Models
{
    public class ProductsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExpirationDate { get; set; }
        public double ProductCost { get; set; }
        public double? ProductPrice { get; set; }
        public double?  Profit { get; set; }

    }
}
