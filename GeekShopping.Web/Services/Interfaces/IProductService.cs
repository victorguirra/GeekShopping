using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductModel>> FindAll(string token);
        public Task<ProductModel> FindById(Int64 id, string token);
        public Task<ProductModel> Create(ProductModel product, string token);
        public Task<ProductModel> Update(ProductModel product, string token);
        public Task<bool> Delete(Int64 id, string token);
    }
}
