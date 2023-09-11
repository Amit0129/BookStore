using BookStore.Books.Context;
using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Model;

namespace BookStore.Books.Service
{
    /// <summary>
    /// Book service
    /// </summary>
    public class BookService : IBookService
    {
        private readonly BookDBContext dBContext;
        public BookService(BookDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        /// <summary>
        /// Add book 
        /// </summary>
        /// <param name="book">Insertbook Model</param>
        /// <returns>Book info</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all book deatils
        /// </summary>
        /// <returns>All book Details</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get book details by book id
        /// </summary>
        /// <param name="bookId">Exiting book BookId</param>
        /// <returns>Book Details</returns>
        public BookEntity GetBookById(long bookId)
        {
            try
            {
                var bookInfo = dBContext.Books.FirstOrDefault(x => x.BookID == bookId);
                if (bookInfo == null)
                {
                    return null;
                }
                return bookInfo;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="updateBook">Book model</param>
        /// <param name="bookId">Exiting book id/param>
        /// <returns>Updated book Details</returns>
        public BookEntity UpdateBookInfo(InsertBookModel updateBook, long bookId)
        {
            try
            {
                var bookInfo = dBContext.Books.FirstOrDefault(x => x.BookID == bookId);
                if (bookInfo == null)
                {
                    return null;
                }
                bookInfo.BookName = updateBook.BookName;
                bookInfo.Description = updateBook.Description;
                bookInfo.Author = updateBook.Author;
                bookInfo.Quantity = updateBook.Quantity;
                bookInfo.DiscountPrice = updateBook.DiscountPrice;
                bookInfo.ActualPrice = updateBook.ActualPrice;
                dBContext.SaveChanges();
                return bookInfo;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Book details
        /// </summary>
        /// <param name="bookId">Exiting book id</param>
        /// <returns>boolean value for sucess of failuer</returns>
        public bool DeleteBook(long bookId)
        {
            try
            {
                var deletedBookInfo = dBContext.Books.FirstOrDefault(x => x.BookID == bookId);
                if (deletedBookInfo == null)
                {
                    return false;
                }
                dBContext.Remove(deletedBookInfo);
                dBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
