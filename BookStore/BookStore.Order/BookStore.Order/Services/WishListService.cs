using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Microsoft.EntityFrameworkCore;

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
                    var presentAlredy = await context.WishLists.FirstOrDefaultAsync(x => x.BookID == wishList.BookID && x.UserID == wishList.UserID);
                    if (bookId != null && userInfo.UserID != null && presentAlredy == null)
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
            var wishListDetails = await context.WishLists.Where(x=>x.UserID == userInfo.UserID).ToListAsync();
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

        public async Task<bool> DeleteWishList(long bookId, string token)
        {
            UserEntity userInfo = await userService.GetUserProfile(token);
            var wishListInfo = await context.WishLists.FirstOrDefaultAsync(x => x.BookID == bookId && x.UserID == userInfo.UserID);
            if (wishListInfo == null)
            {
                return false;
            }

            context.WishLists.Remove(wishListInfo);
            context.SaveChanges();
            return true;
        }

    }
}
