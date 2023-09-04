using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Model;

namespace BookStore.Order.Services
{
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
        public async Task<OrderEntity> PlaceOrder(OrderRegisterModel registerModel,string token)
        {
            try
            {
                UserEntity userInfo = await userService.GetUserProfile(token);
                OrderEntity orderInfo = new OrderEntity()
                {
                    UserID = userInfo.UserID,
                    BookID = registerModel.BookID,
                    OrderQty= registerModel.OrderQty,
                    Book = await bookService.GetBookById(registerModel.BookID),
                    User = userInfo
                };
                orderInfo.OrderAmount = orderInfo.Book.DiscountPrice * registerModel.OrderQty;
                dBContext.Ordres.Add(orderInfo);
                dBContext.SaveChanges();
                return orderInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
