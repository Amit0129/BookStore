using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Order.Controllers
{
    /// <summary>
    /// Order Crotroller
    /// </summary>
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
            response = new ResponseEntity();
        }
        /// <summary>
        /// Place Order
        /// </summary>
        /// <param name="bookId">Exiting book id</param>
        /// <param name="Qty">Quantity of book</param>
        /// <returns>Order Information</returns>
        [Authorize]
        [HttpPost("Placeorder/{bookId}/{Qty}")]
        public async Task<ResponseEntity> PlaceOrder(long bookId, int Qty)
        {
            try
            {
                string token = Request.Headers.Authorization.ToString();
                token = token.Substring("Bearer".Length);
                OrderEntity order = await orderService.PlaceOrder(bookId, Qty, token);
                if (order == null)
                {
                    response.IsSucess = false;
                    response.Message = "PlaceOrder Failed";
                    return response;
                }
                response.Data = order;
                response.Message = "PlaceOrder Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// View Order details of a user
        /// </summary>
        /// <returns>Order details of a user</returns>
        [Authorize]
        [HttpGet]
        public async Task<ResponseEntity> ViewOrderDetails()
        {
            try
            {
                string token = Request.Headers.Authorization.ToString();
                token = token.Substring("Bearer".Length);
                UserEntity userInfo = await userService.GetUserProfile(token);
                long userId = userInfo.UserID;
                var orderInfo = await orderService.ViewOrderDetails(token);
                if (orderInfo == null)
                {
                    response.Message = "Retrive Failed";
                    response.IsSucess = false;
                    return response;
                }
                else
                {
                    response.IsSucess = true;
                    response.Message = "Retrive Sucessfull";
                    response.Data = orderInfo;
                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
       /// <summary>
       /// Cancel order of a user
       /// </summary>
       /// <param name="bookId">book id from a placed order</param>
       /// <returns>Boolean value for sucess of failure </returns>
        [HttpDelete("CancelOrder/{bookId}")]
        public async Task<ResponseEntity> CancelOrder(long bookId)
        {
            try
            {
                string token = Request.Headers.Authorization.ToString();
                token = token.Substring("Bearer".Length);
                var cancelOrder = await orderService.CancelOrder(bookId, token);
                if(cancelOrder == false)
                {
                    response.IsSucess = false;
                    response.Message = "Cancel Order Failed";
                    return response;
                }
                response.IsSucess = true;
                response.Message = "Cancel Order Sucessfull";
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get book deatils by id
        /// </summary>
        /// <param name="bookId">Exiting book Id</param>
        /// <returns>Book details</returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get user details of login user
        /// </summary>
        /// <returns></returns>
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
