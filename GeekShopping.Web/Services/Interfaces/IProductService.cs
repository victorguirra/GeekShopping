using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductModel>> FindAll();
        public Task<ProductModel> FindById(Int64 id);
        public Task<ProductModel> Create(ProductModel product);
        public Task<ProductModel> Update(ProductModel product);
        public Task<bool> Delete(Int64 id);
    }
}
