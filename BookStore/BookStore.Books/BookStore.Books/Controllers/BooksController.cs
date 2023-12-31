﻿using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Books.Controllers
{
    /// <summary>
    /// Book controller
    /// </summary>
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
        /// <summary>
        /// Add book to database controller Endpoint
        /// </summary>
        /// <param name="bookModel">AddBook model</param>
        /// <returns>Book info In Response Model Format</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get All book from database Controller EndPoint
        /// </summary>
        /// <returns>All book info</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get a particular book Info from database Controller EndPoint
        /// </summary>
        /// <param name="bookId">Book id</param>
        /// <returns>Book Info In Response model Format</returns>
        [HttpGet("GetById/{bookId}")]
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Update Book Info
        /// </summary>
        /// <param name="updateBook">Update Model</param>
        /// <param name="bookId">book id</param>
        /// <returns>Book Details with SMD Formart</returns>
        /// <exception cref="Exception"></exception>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="bookId">Book id</param>
        /// <returns>Boolean for sucess or not In SMD Formart</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
