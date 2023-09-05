using BookStore.Order.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Order.Context
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<OrderEntity> Ordres { get; set; }
    }
}
