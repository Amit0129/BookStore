﻿using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;
        private readonly IOrderService orderService;
        private readonly ResponseEntity response;
        public OrderController(IUserService userService, IBookService bookService, IOrderService orderService)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.orderService = orderService;
            response= new ResponseEntity();
        }

        //Get Book Details
        [HttpGet("GetBookById/{bookId}")]
        public async Task<ResponseEntity> GetBookById(long bookId)
        {
            try
            {
                BookEntity bookInfo = await bookService.GetBookById(bookId);
                if (bookInfo == null)
                {
                    response.IsSucess = false;
                    response.Message = "Data Retrive Failed";
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
        [HttpGet("GetUserDetails")]
        public async Task<ResponseEntity> GetUserProfile()
        {
            try
            {
                string token = Request.Headers.Authorization.ToString(); // token have "Bearer " we need to remove that
                token = token.Substring("Bearer".Length); // now we have jwt token - without Bearer and a space
                UserEntity userInfo = await userService.GetUserProfile(token);
                if (userInfo == null)
                {
                    response.IsSucess = false;
                    response.Message = "Data Retrive Failed";
                    return response;
                }
                response.Data = userInfo;
                response.Message = "Data Retrive Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
