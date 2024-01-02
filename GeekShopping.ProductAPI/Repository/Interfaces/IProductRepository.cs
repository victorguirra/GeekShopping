using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductVO>> FindAll();
        public Task<ProductVO> FindById(Int64 id);
        public Task<ProductVO> Create(ProductVO productVO);
        public Task<ProductVO> Update(ProductVO productVO);
        public Task<bool> Delete(Int64 id);
    }
}
