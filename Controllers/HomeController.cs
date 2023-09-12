using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ProductDBContext ProductDBContext = new ProductDBContext();
        public List<Product> Products { get; set; } = new List<Product>();
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task< IActionResult> Index()
        {
   
            Logger.Info("This is an info log message.");
            Products = await ProductDBContext.Products.ToListAsync();

            return View(Products);
        }
    }
}