using BookStore.Admin.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Admin.Context
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<AdminEntity> Admin { get; set; }
    }
}
