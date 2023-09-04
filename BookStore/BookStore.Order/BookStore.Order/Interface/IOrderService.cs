using BookStore.Order.Entity;
using BookStore.Order.Model;

namespace BookStore.Order.Interface
{
    public interface IOrderService
    {
        public Task<OrderEntity> PlaceOrder(OrderRegisterModel registerModel, string token);
    }
}
