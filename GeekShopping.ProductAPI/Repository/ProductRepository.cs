using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Models;
using GeekShopping.ProductAPI.Models.Context;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(Int64 id)
        {
            Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync() ?? new Product();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO productVO)
        {
            Product newProduct = _mapper.Map<Product>(productVO);
            
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(newProduct);
        }

        public async Task<ProductVO> Update(ProductVO productVO)
        {
            Product newProduct = _mapper.Map<Product>(productVO);

            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(newProduct);
        }

        public async Task<bool> Delete(Int64 id)
        {
            try
            {
                Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync() ?? new Product();

                if (product.Id <= 0)
                    return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
