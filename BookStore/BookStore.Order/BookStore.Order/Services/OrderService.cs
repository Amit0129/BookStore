using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Order.Services
{
    /// <summary>
    /// Order service class
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly OrderDBContext dBContext;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public OrderService(OrderDBContext dBContext, IUserService userService, IBookService bookService)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this.bookService = bookService;
        }
        /// <summary>
        /// Add Order to data base
        /// </summary>
        /// <param name="bookId">Exiting Book id</param>
        /// <param name="Qty">Number of book</param>
        /// <param name="token">Jwt Toket Get from header</param>
        /// <returns>Order Details</returns>
        public async Task<OrderEntity> PlaceOrder(long bookId, int Qty, string token)
        {
            try
            {
                UserEntity userInfo = await userService.GetUserProfile(token);
                OrderEntity orderInfo = new OrderEntity()
                {
                    UserID = userInfo.UserID,
                    BookID = bookId,
                    OrderQty = Qty,
                    Book = await bookService.GetBookById(bookId),
                    User = userInfo
                };
                orderInfo.OrderAmount = orderInfo.Book.DiscountPrice * Qty;
                if (bookId != null && userInfo.UserID != null && Qty > 0)
                {
                    dBContext.Ordres.Add(orderInfo);
                    dBContext.SaveChanges();
                    return orderInfo;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// View an Order
        /// </summary>
        /// <param name="token">Jwt Toket Get from header</param>
        /// <returns>Order Deatils</returns>
        public async Task<IEnumerable<OrderEntity>> ViewOrderDetails(string token)
        {
            try
            {
                UserEntity userInfo = await userService.GetUserProfile(token);
                var userId = userInfo.UserID;
                var orderDetails = await dBContext.Ordres.Where(x => x.UserID == userId).ToListAsync();
                if (orderDetails == null)
                {
                    return null;
                }
                else
                {
                    foreach (var order in orderDetails)
                    {
                        order.Book = await bookService.GetBookById(order.BookID);
                        order.User = userInfo;
                        order.OrderAmount = order.OrderQty * order.Book.DiscountPrice;
                    }
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="bookId">Orderd Book Id</param>
        /// <param name="token">Jwt Toket Get from header</param>
        /// <returns>Cancel Order info</returns>
        public async Task<bool> CancelOrder(long bookId,string token)
        {

            try
            {
                UserEntity userInfo = await userService.GetUserProfile(token);
                OrderEntity orderInfo = await dBContext.Ordres.FirstOrDefaultAsync(x => x.UserID == userInfo.UserID && x.BookID == bookId);
                if (orderInfo == null)
                {
                    return false;
                }
                dBContext.Ordres.Remove(orderInfo);
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
