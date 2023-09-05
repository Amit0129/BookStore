using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IOrderService
    {
        public Task<OrderEntity> PlaceOrder(long bookId, int Qty, string token);
        public Task<IEnumerable<OrderEntity>> ViewOrderDetails(string token);
        public Task<bool> CancelOrder(long bookId,string token);
    }
}
