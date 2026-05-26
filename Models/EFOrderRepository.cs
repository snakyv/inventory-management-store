using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly StoreDbContext context;

        public EFOrderRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders =>
            context.Orders
                .Include(o => o.Lines);

        public void SaveOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order? dbEntry = context.Orders
                    .FirstOrDefault(o => o.OrderID == order.OrderID);

                if (dbEntry != null)
                {
                    dbEntry.Name = order.Name;
                    dbEntry.Line1 = order.Line1;
                    dbEntry.Line2 = order.Line2;
                    dbEntry.Line3 = order.Line3;
                    dbEntry.City = order.City;
                    dbEntry.State = order.State;
                    dbEntry.Zip = order.Zip;
                    dbEntry.Country = order.Country;
                    dbEntry.GiftWrap = order.GiftWrap;
                    dbEntry.Shipped = order.Shipped;
                }
            }

            context.SaveChanges();
        }
    }
}