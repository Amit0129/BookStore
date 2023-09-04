using BookStore.Books.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Books.Context
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BookEntity> Books { get; set; }
    }
}
