using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string _basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAll(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.GetAsync(_basePath);

            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindById(long id, string token)
        {
            string path = $"{_basePath}/{id}";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.GetAsync(path);
            
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsJson(_basePath, product);
            
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<ProductModel> Update(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PutAsJson(_basePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> Delete(long id, string token)
        {
            string path = $"{_basePath}/{id}";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.DeleteAsync(path);

            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API");
        }
    }
}
