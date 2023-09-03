using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        //AddBooks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBook(InsertBookModel bookModel)
        {
            try
            {
                var books = bookService.AddBook(bookModel);
                if (books == null)
                {
                    return BadRequest(new { sucess = false, message = "Book Added Failed" });
                }
                return Ok(new { sucess = true, message = "Book Added Sucessfull", data = books });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getallbooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var book = bookService.GetAllBooks();
                if (book ==  null)
                {
                    return BadRequest(new { sucess = false, message = "Retrive Failed" });
                }
                return Ok(new { sucess = true, message = "Retrive Sucessfull", data = book });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get book by id
        [HttpGet("GetById/{bookId}")]
        public IActionResult GetBookById(long bookId)
        {
            try
            {
                var bookInfo = bookService.GetBookById(bookId);
                if (bookInfo == null)
                {
                    return BadRequest(new { sucess = false, message = "Retrive Failed" });
                }
                return Ok(new { sucess = true, message = "Retrive Sucessfull", data = bookInfo });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Update book
        [Authorize(Roles ="Admin")]
        [HttpPut("update/{bookId}")]
        public IActionResult UpdateBookInfo(InsertBookModel updateBook, long bookId)
        {
            try
            {
                var bookInfo = bookService.UpdateBookInfo(updateBook, bookId);
                if (bookInfo == null)
                {
                    return BadRequest(new { sucess = false, message = "Update Failed" });
                }
                return Ok(new { sucess = true, message = "Update Sucessfull", data = bookInfo });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
