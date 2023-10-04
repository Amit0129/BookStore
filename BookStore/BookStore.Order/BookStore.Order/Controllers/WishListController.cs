using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly OrderDBContext context;
        private readonly IWishListService wishListService;
        private readonly ResponseEntity response;
        public WishListController(IWishListService wishListService, OrderDBContext context)
        {
            this.context = context;
            this.wishListService = wishListService;
            response = new ResponseEntity();
        }
        [HttpPost("AddWishList/{bookId}")]
        public async Task<ResponseEntity> AddToWishList(long bookId)
        {
            try
            {
                string token = Request.Headers.Authorization.ToString();
                token = token.Substring("Bearer".Length);
                WishListEntity wishList = await wishListService.AddToWishList(bookId, token);
                if (wishListService == null)
                {
                    response.IsSucess = false;
                    response.Message = "Add to WishList Failed";
                    return response;
                }
                response.Data = wishList;
                response.Message = "PlaceOrder Sucessfull";
                response.IsSucess = true;
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetWishList")]
        public async Task<ResponseEntity> GetWishList()
        {
            try
            {
                string token = Request.Headers.Authorization.ToString();
                token = token.Substring("Bearer".Length);
                var wishList = await wishListService.GetWishList(token);
                if (wishList == null)
                {
                    response.IsSucess = false;
                    response.Message = "Get Wishlist of user Failed";
                    return response;
                }
                response.IsSucess = true;
                response.Message = "Get Wishlist of user Sucessfull";
                response.Data = wishList;
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
