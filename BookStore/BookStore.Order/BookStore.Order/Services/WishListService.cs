using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;

namespace BookStore.Order.Services
{
    public class WishListService : IWishListService
    {
        private readonly OrderDBContext context;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        public WishListService(OrderDBContext context,IUserService userService, IBookService bookService)
        {
            this.context = context;
            this.userService = userService;
            this.bookService = bookService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId">Book Id of the book user want to wish list</param>
        /// <param name="token">Jwt token for authorization</param>
        /// <returns></returns>
        public async Task<WishListEntity> AddToWishList(long bookId,string token)
        {
            try
            {
                var bookInfo = bookService.GetBookById(bookId);
                UserEntity userInfo = await userService.GetUserProfile(token);
                if (userInfo != null)
                {
                    var wishList = new WishListEntity()
                    {
                        UserID = userInfo.UserID,
                        BookID = bookId,
                        Book = await bookService.GetBookById(bookId),
                        User = userInfo
                    };
                    if (bookId != null && userInfo.UserID != null)
                    {
                        context.WishLists.Add(wishList);
                        context.SaveChanges();
                        return wishList;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<WishListEntity>> GetWishList(string token)
        {
            UserEntity userInfo = await userService.GetUserProfile(token);
            var wishListDetails = context.WishLists.Where(x=>x.UserID == userInfo.UserID).ToList();
            if (wishListDetails == null)
            {
                return null;
            }
            foreach (var item in wishListDetails)
            {
                item.Book = await bookService.GetBookById(item.BookID);
                item.User = userInfo;
            }
            return wishListDetails;
        }
    }
}
