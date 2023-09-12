using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Linq;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        public  List<Product> _products = new List<Product>();
        public List<Category> _categories = new List<Category>();
        public List<Seller> _sellers = new List<Seller>();
        public List<Delivery> _delivery = new List<Delivery>();

        public ProductDBContext _db = new ProductDBContext();
        public ProductController()
        {
            _products =  _db.Products.ToList();
            _categories =  _db.Categories.ToList();
            _sellers =  _db.Sellers.ToList();
            _delivery =  _db.Deliveries.ToList();
        }

        public IActionResult Index()
        {
            var filter = new ProductFilter();
            filter.MinPrice = _products.Min(p => p.Price);
            filter.MaxPrice = _products.Max(p => p.Price);
            var viewModel = new ProductViewModel
            {
                Products = _products,
                Filter = filter
            };

            var brands = _products.Select(p => p.Category).Distinct().ToList();
            var minPrice = _products.Min(p => p.Price);
            var maxPrice = _products.Max(p => p.Price);

            ViewBag.Categories = brands;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(ProductFilter filter)
        {
            var brands = _products.Select(p => p.Category).Distinct().ToList();
 
            ViewBag.Categories = brands;
            ViewBag.MinPrice = 9;
            ViewBag.MaxPrice = 2222;

            var filteredProducts = _products;

            if (filter.Categories != null && filter.Categories.Count() > 0)
            {
                filteredProducts = filteredProducts.Where(p => filter.Categories.Contains(p.Category.Name)).ToList();
            }

            if (filter.MinPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= filter.MinPrice).ToList();
            }

            if (filter.MaxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= filter.MaxPrice).ToList();
            }

            var viewModel = new ProductViewModel
            {
                Products = filteredProducts,
                Filter = filter
            };

            ViewBag.Filter = filter;
            return View(viewModel);
        }

        //private async Task GetProductList()
        //{
        //_products = await _db.Products.ToListAsync();
        //_categories = await _db.Categories.ToListAsync();
        //_sellers = await _db.Sellers.ToListAsync();
        //_delivery = await _db.Deliveries.ToListAsync();


        //var category1 = new Category
        //{
        //    Id = 1,
        //    Name = "Смартфоны"
        //};
        //var category2 = new Category
        //{
        //    Id = 2,
        //    Name = "Ноутбуки"
        //};
        //var seller1 = new Seller
        //{
        //    Id = 1,
        //    Name = "Apple Store",
        //    Url = "https://www.apple.com",
        //    Margin = 15.0m
        //};
        //var seller2 = new Seller
        //{
        //    Id = 2,
        //    Name = "Lenovo Store",
        //    Url = "https://www.lenovo.com",
        //    Margin = 25.0m
        //};
        //var delivery1 = new Delivery
        //{
        //    Id = 1,
        //    Name = "Экспресс-доставка",
        //    Price = 10.0m,
        //    ArrivalDateDays = 1
        //};
        //var delivery2 = new Delivery
        //{
        //    Id = 2,
        //    Name = "Супер-экспресс-доставка",
        //    Price = 15.0m,
        //    ArrivalDateDays = 1
        //};

        //var product1 = new Product
        //{
        //    Id = 1,
        //    Name = "Смартфон iPhone 13",
        //    Description = "Популярный смартфон от Apple.",
        //    ImageUrl = "https://content2.rozetka.com.ua/goods/images/big/334936519.jpg",
        //    Price = 799.99m,
        //    Category = category1,
        //    Delivery = delivery1,
        //    Seller = seller1
        //};
        //var product2 = new Product
        //{
        //    Id = 2,
        //    Name = "Ноутбук Dell XPS 15",
        //    Description = "Мощный ноутбук для профессионалов.",
        //    ImageUrl = "https://scdn.comfy.ua/89fc351a-22e7-41ee-8321-f8a9356ca351/https://cdn.comfy.ua/media/catalog/product/a/n/an515-57_1650.jpg/w_600",
        //    Price = 1499.99m,
        //    Category = category2,
        //    Delivery = delivery2,
        //    Seller = seller2
        //};



        //_products.Add(product1);
        //    _products.Add(product2);
        //    _products.Add(product1);
        //    _products.Add(product1);
        //    _products.Add(product2);
        //    _products.Add(product1);
        //    _products.Add(product2);
        //    _products.Add(product2);

        //    _delivery.Add(delivery2);
        //    _delivery.Add(delivery1);
        //    _sellers.Add(seller1);
        //    _sellers.Add(seller2);
        //    _categories.Add(category1);
        //    _categories.Add(category2);


        //    _db.Products.AddRange(_products);

        //    _db.Products.AddRange(_products);

        //    _db.Products.AddRange(_products);
        //    _db.Deliveries.AddRange(_delivery);
        //    _db.Sellers.AddRange(_sellers);
        //    _db.Categories.AddRange(_categories);
        //    _db.SaveChanges();
        //}
    }
}
