using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IBookService
    {
        public Task<BookEntity> GetBookById(long bookId);
    }
}
