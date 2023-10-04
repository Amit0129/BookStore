using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IWishListService
    {
        public Task<WishListEntity> AddToWishList(long bookId, string token);
        public Task<IEnumerable<WishListEntity>> GetWishList(string token);
        public Task<bool> DeleteWishList(long bookId, string token);
    }
}
