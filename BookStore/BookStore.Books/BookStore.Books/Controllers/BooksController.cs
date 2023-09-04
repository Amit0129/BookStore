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
        public ResponseEntity response;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
            response = new ResponseEntity();
        }
        //AddBooks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ResponseEntity AddBook(InsertBookModel bookModel)
        {
            try
            {
                var books = bookService.AddBook(bookModel);
                if (books == null)
                {
                    response.Message = "Book Added Failed";
                    response.IsSucess = false;
                    return response;
                }
                response.Data = books;
                response.Message = "Book Added Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getallbooks")]
        public ResponseEntity GetAllBooks()
        {
            try
            {
                var book = bookService.GetAllBooks();
                if (book == null)
                {
                    response.Message = "Data Retrive Failed";
                    response.IsSucess = false;
                    return response;
                }
                response.Data = book;
                response.Message = "Data Retrive Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get book by id
        [HttpGet("GetById")]
        public ResponseEntity GetBookById(long bookId)
        {
            try
            {
                var bookInfo = bookService.GetBookById(bookId);
                if (bookInfo == null)
                {
                    response.Message = "Data Retrive Failed";
                    response.IsSucess = false;
                    return response;
                }
                response.Data = bookInfo;
                response.Message = "Data Retrive Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Update book
        [Authorize(Roles = "Admin")]
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
        //Delete boook
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBook/{bookId}")]
        public IActionResult DeleteBook(long bookId)
        {
            try
            {
                var book = bookService.DeleteBook(bookId);
                if (book == false)
                {
                    return BadRequest(new { sucess = false, message = "Delete Failed" });
                }
                return Ok(new { sucess = true, message = "Delete Sucessfull" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
