using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string _basePath = "api/v1/product";

        public ProductService()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            HttpResponseMessage response = await _client.GetAsync(_basePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindById(long id)
        {
            string path = $"{_basePath}/{id}";
            HttpResponseMessage response = await _client.GetAsync(path);
            
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel product)
        {
            HttpResponseMessage response = await _client.PostAsJson(_basePath, product);
            
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<ProductModel> Update(ProductModel product)
        {
            HttpResponseMessage response = await _client.PutAsJson(_basePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> Delete(long id)
        {
            string path = $"{_basePath}/{id}";
            HttpResponseMessage response = await _client.DeleteAsync(path);

            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API");
        }
    }
}
