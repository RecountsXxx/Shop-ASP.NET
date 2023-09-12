using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;

namespace Shop.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ProductDBContext _context = new ProductDBContext();

        public AdminPanelController()
        {
            
        }

        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sellers = _context.Sellers.ToList();
            ViewBag.Deliveries = _context.Deliveries.ToList();


            var categories = _context.Categories.ToList();
            var sellers = _context.Sellers.ToList();
            var deliveries = _context.Deliveries.ToList();

            var viewModel = new ProductViewModelAdmin
            {
                Category = categories,
                Seller = sellers,
                Delivery = deliveries
            };
            return View(viewModel);

        }

        public IActionResult Products()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.Delivery)
                .ToListAsync();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            product.Category = _context.Categories.Where(x => x.Id == product.Category.Id).FirstOrDefault();
            product.Delivery = _context.Deliveries.Where(x => x.Id == product.Delivery.Id).FirstOrDefault();
            product.Seller = _context.Sellers.Where(x => x.Id == product.Seller.Id).FirstOrDefault();
            var viewModel = new ProductViewModelAdmin
            {
                Category = _context.Categories.ToList(),
                Seller = _context.Sellers.ToList(),
                Delivery = _context.Deliveries.ToList()
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            HomeController.Logger.Info("Create product: " + product.Name);

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            HomeController.Logger.Info("Created category: " + category.Name);
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(Delivery delivery)
        {
            HomeController.Logger.Info("Created delivery: " + delivery.Name);
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller(Seller seller)
        {
            HomeController.Logger.Info("Created seller: " + seller.Name);
            _context.Sellers.Add(seller);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            HomeController.Logger.Info("Deleted product: " + product.Name);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult DeleteSeller(int sellerId)
        {
            var seller = _context.Sellers.Find(sellerId);

            if (seller != null)
            {
                _context.Products.RemoveRange(_context.Products.Where(x => x.Seller.Id == sellerId));
                _context.Sellers.Remove(seller);
                _context.SaveChanges();
            }
            HomeController.Logger.Info("Deleted seller: " + seller.Name);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult DeleteDelivery(int deliveryId)
        {
            var delivery = _context.Deliveries.Find(deliveryId);

            if (delivery != null)
            {
                _context.Products.RemoveRange(_context.Products.Where(x => x.Delivery.Id == deliveryId));
                _context.Deliveries.Remove(delivery);
                _context.SaveChanges();
            }
            HomeController.Logger.Info("Deleted delivery: " + delivery.Name);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category != null)
            {
                _context.Products.RemoveRange(_context.Products.Where(x => x.Category.Id == categoryId));
                _context.Categories.Remove(category);
                _context.SaveChanges();

            }
            HomeController.Logger.Info("Deleted category: " + category.Name);
            return RedirectToAction("index");
        }
    }
}
