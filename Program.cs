using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("InventoryConnection"));
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
    name: "catpage",
    pattern: "{category}/Page{productPage:int}",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "page",
    pattern: "Page{productPage:int}",
    defaults: new { controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
    name: "category",
    pattern: "{category}",
    defaults: new { controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
    name: "pagination",
    pattern: "Inventory/Page{productPage:int}",
    defaults: new { controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.MapBlazorHub();

app.MapFallbackToPage(
    pattern: "/admin/{*catchall}",
    page: "/Admin/Index");

app.Run();