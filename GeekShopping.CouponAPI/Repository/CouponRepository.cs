using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Models;
using GeekShopping.CouponAPI.Models.Context;
using GeekShopping.CouponAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CouponRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            Coupon coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.CouponCode == couponCode);
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}
