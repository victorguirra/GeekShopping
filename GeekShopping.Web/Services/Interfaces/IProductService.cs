using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> FindAll(string token);
        public Task<ProductViewModel> FindById(Int64 id, string token);
        public Task<ProductViewModel> Create(ProductViewModel product, string token);
        public Task<ProductViewModel> Update(ProductViewModel product, string token);
        public Task<bool> Delete(Int64 id, string token);
    }
}
