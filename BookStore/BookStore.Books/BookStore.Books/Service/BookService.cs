using BookStore.Books.Context;
using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Model;

namespace BookStore.Books.Service
{
    public class BookService : IBookService
    {
        private readonly BookDBContext dBContext;
        public BookService(BookDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public BookEntity AddBook(InsertBookModel book)
        {
            try
            {
                BookEntity bookEntity = new BookEntity()
                {
                    BookName = book.BookName,
                    Description = book.Description,
                    Author = book.Author,
                    Quantity = book.Quantity,
                    DiscountPrice = book.DiscountPrice,
                    ActualPrice = book.ActualPrice
                };
                dBContext.Books.Add(bookEntity);
                dBContext.SaveChanges();
                return bookEntity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //GetAllBooks
        public IEnumerable<BookEntity> GetAllBooks()
        {
            try
            {
                var booksList = dBContext.Books.ToList();
                if (booksList == null)
                {
                    return null;
                }
                return booksList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get book by id
        public BookEntity GetBookById(long bookId)
        {
            try
            {
                var bookInfo = dBContext.Books.FirstOrDefault(x => x.BookID == bookId);
                if (bookInfo==null)
                {
                    return null;
                }
                return bookInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Update Book
        public BookEntity UpdateBookInfo(InsertBookModel updateBook,long bookId)
        {
            try
            {
                var bookInfo = dBContext.Books.FirstOrDefault(x => x.BookID == bookId);
                if (bookInfo ==  null)
                {
                    return null;
                }
                bookInfo.BookName = updateBook.BookName;
                bookInfo.Description = updateBook.Description;
                bookInfo.Author = updateBook.Author;
                bookInfo.Quantity= updateBook.Quantity;
                bookInfo.DiscountPrice= updateBook.DiscountPrice;
                bookInfo.ActualPrice= updateBook.ActualPrice;
                dBContext.SaveChanges();
                return bookInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
