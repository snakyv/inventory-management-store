using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreRepository repository;

        public CartModel(IStoreRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }

        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string? returnUrl)
        {
            ReturnUrl = string.IsNullOrEmpty(returnUrl)
                ? "/"
                : returnUrl;
        }

        public IActionResult OnPost(int inventoryId, string? returnUrl)
        {
            Inventory? inventoryItem = repository.InventoryItems
                .FirstOrDefault(i => i.InventoryID == inventoryId);

            if (inventoryItem != null)
            {
                Cart.AddItem(inventoryItem, 1);
            }

            return RedirectToPage(new
            {
                returnUrl = string.IsNullOrEmpty(returnUrl)
                    ? "/"
                    : returnUrl
            });
        }

        public IActionResult OnPostRemove(int inventoryId, string? returnUrl)
        {
            Cart.RemoveLine(inventoryId);

            return RedirectToPage(new
            {
                returnUrl = string.IsNullOrEmpty(returnUrl)
                    ? "/"
                    : returnUrl
            });
        }
    }
}