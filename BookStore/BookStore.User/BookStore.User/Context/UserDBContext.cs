using BookStore.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.User.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
    }
}
