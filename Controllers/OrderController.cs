using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repository;

        private readonly Cart cart;

        public OrderController(
            IOrderRepository repoService,
            Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError(
                    "",
                    "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.Select(line => new OrderLine
                {
                    InventoryID = line.InventoryItem.InventoryID,
                    ItemName = line.InventoryItem.Name,
                    Price = line.InventoryItem.Price,
                    Quantity = line.Quantity
                }).ToList();

                repository.SaveOrder(order);

                cart.Clear();

                return RedirectToPage(
                    "/Completed",
                    new { orderId = order.OrderID });
            }

            return View(order);
        }
    }
}