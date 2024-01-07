using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<CartVO> FindCartByUserId(string userId);
        Task<CartVO> SaveOrUpdate(CartVO cart);
        Task<bool> RemoveFromCart(Int64 cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
