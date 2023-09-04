using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;

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
    }
}
