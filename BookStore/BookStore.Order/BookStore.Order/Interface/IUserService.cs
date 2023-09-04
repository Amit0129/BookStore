using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IUserService
    {
        public Task<UserEntity> GetUserProfile(string token);
    }
}
