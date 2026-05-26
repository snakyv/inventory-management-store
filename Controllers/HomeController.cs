using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository repository;

        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string? category, int productPage = 1)
        {
            IQueryable<Inventory> filteredItems = repository.InventoryItems
                .Where(i => category == null
                    || (i.Part != null && i.Part.Name == category));

            return View(new InventoryListViewModel
            {
                InventoryItems = filteredItems
                    .OrderBy(i => i.InventoryID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredItems.Count()
                },

                CurrentCategory = category
            });
        }
    }
}