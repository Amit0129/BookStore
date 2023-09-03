using BookStore.Books.Entity;
using BookStore.Books.Model;

namespace BookStore.Books.Interface
{
    public interface IBookService
    {
        public BookEntity AddBook(InsertBookModel book);
        public IEnumerable<BookEntity> GetAllBooks(); 
        public BookEntity GetBookById(long bookId);
        public BookEntity UpdateBookInfo(InsertBookModel updateBook, long bookId);
    }
}
