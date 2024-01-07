using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string _basePath = "api/v1/cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            string path = $"{_basePath}/find-cart/{userId}";
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.GetAsync(path);

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            string path = $"{_basePath}/add-cart";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsJson(path, cart);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel cart, string token)
        {
            string path = $"{_basePath}/update-cart";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PutAsJson(path, cart);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> RemoveFromCart(Int64 cartId, string token)
        {
            string path = $"{_basePath}/remove-cart/{cartId}";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.DeleteAsync(path);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API");

            throw new NotImplementedException();
        }

        public async Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }
    }
}
