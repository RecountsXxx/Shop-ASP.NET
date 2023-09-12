using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using OnlineShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Shop.Models;
//var _db = new ProductDBContext();
//var _products = await _db.Products.ToListAsync();
//var _categories = await _db.Categories.ToListAsync();
//var _sellers = await _db.Sellers.ToListAsync();
//var _delivery = await _db.Deliveries.ToListAsync();


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
//_products.Add(product2);
//_products.Add(product1);
//_products.Add(product1);
//_products.Add(product2);
//_products.Add(product1);
//_products.Add(product2);
//_products.Add(product2);

//_delivery.Add(delivery2);
//_delivery.Add(delivery1);
//_sellers.Add(seller1);
//_sellers.Add(seller2);
//_categories.Add(category1);
//_categories.Add(category2);


//_db.Products.AddRange(_products);

//_db.Products.AddRange(_products);

//_db.Products.AddRange(_products);
//_db.Deliveries.AddRange(_delivery);
//_db.Sellers.AddRange(_sellers);
//_db.Categories.AddRange(_categories);
//_db.SaveChanges();


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

var connectionString = "Data Source=shopdb.db";
builder.Services.AddDbContext<ProductDBContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ProductDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Указывает путь к странице входа
    });
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Cart>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ProductDBContext>();

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    dbContext.Database.Migrate();

    DbInitializer.Initialize(services);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();


app.Run();