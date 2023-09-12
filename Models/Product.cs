namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set;  }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Seller Seller { get; set; }
        public Delivery Delivery { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Margin { get; set; } //это % наценки
    }
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ArrivalDateDays { get; set; }
    }

    public class ProductFilter
    {
        public List<string> Categories { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public ProductFilter Filter { get; set; }
    }

    public class ProductViewModelAdmin
    {
        public Product Product { get; set; }
        public List<Category> Category { get; set; }
        public List<Delivery> Delivery { get; set; }
        public List<Seller> Seller { get; set; }
    }
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }

}
