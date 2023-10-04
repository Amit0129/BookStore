using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IWishListService
    {
        public Task<WishListEntity> AddToWishList(long bookId, string token);
    }
}
