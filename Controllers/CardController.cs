using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Security.Claims;

public class CartController : Controller
{
    public readonly Cart _cart;
    private ProductDBContext _dbContext = new ProductDBContext();
    public CartController(Cart cart)
    {
        _cart = cart;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }
        var cart = _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => c.UserId == userId);
        if(cart == null)
        {
            cart = new Cart();
        }
        return View(cart);
    }
    [HttpPost]
    public IActionResult AddToCart(string productName, string productPrice)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cart = _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };
            _dbContext.Carts.Add(cart);
        }

        var cartItem = new CartItem
        {
            ProductId = 1,
            ProductName = productName,
            Price = Convert.ToDecimal(productPrice.Replace("Price:", "").Replace("$", "").Trim())
        };
    

        cart.Items.Add(cartItem);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int cartItemId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cart = _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => c.UserId == userId);
        var cartItem = cart.Items.FirstOrDefault(item => item.Id == cartItemId);
        if (cartItem != null)
        {
            cart.Items.Remove(cartItem);
            _dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }


        [HttpPost]
    public IActionResult Checkout(string productName, string productPrice)
    {
        return RedirectToAction("Index");
    }

}
