using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory =
                RouteData?.Values["category"]?.ToString();

            return View(repository.InventoryItems
                .Where(i => i.Part != null)
                .Select(i => i.Part!.Name)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}