﻿using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Models;
using GeekShopping.CartAPI.Models.Context;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            CartHeader cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            IEnumerable<CartDetail> cartDetails =  _context.CartDetails
                .Where(x => x.CartHeaderId == cartHeader.Id)
                .Include(c => c.Product);

            Cart cart = new Cart();
            cart.CartHeader = cartHeader;
            cart.CartDetails = cartDetails;

            return _mapper.Map<CartVO>(cart);
        }

        public async Task<CartVO> SaveOrUpdate(CartVO cartVO)
        {
            Cart cart = _mapper.Map<Cart>(cartVO);
            Int64? productId = cartVO.CartDetails?.FirstOrDefault()?.ProductId;
            
            // Checks if the product is already saved in the database if it does not exist then save;
            Product? productCartVO = await _context.Products.FirstOrDefaultAsync(x => x.Id == (Int64)productId);

            if(productCartVO is null)
            {
                Product? product = cart.CartDetails?.FirstOrDefault()?.Product;
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }

            // Check if CartHeader is null;
            CartHeader? cartHeader = await _context.CartHeaders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == cart.CartHeader.UserId);

            if(cartHeader is null)
            {
                // Create CartHeader and CartDetails
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;

                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                // Check if CartDetails has same product
                CartDetail? cartDetail = await _context.CartDetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductId == cart.CartDetails.FirstOrDefault().ProductId && x.CartHeaderId == cartHeader.Id);

                if(cartDetail is null)
                {
                    // Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;

                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;

                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveFromCart(Int64 cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.Id == cartDetailsId);

                int total = _context.CartDetails
                    .Where(x => x.CartHeaderId == cartDetail.CartHeaderId)
                    .Count();

                _context.CartDetails.Remove(cartDetail);

                if(total == 1)
                {
                    CartHeader cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            CartHeader? header = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if(header is not null)
            {
                header.CouponCode = couponCode;

                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            CartHeader? header = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);

            if(header is not null)
            {
                header.CouponCode = string.Empty;

                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            CartHeader cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);

            if(cartHeader is not null)
            {
                IQueryable<CartDetail> cartDetailsToRemove = _context.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id);

                _context.CartDetails.RemoveRange(cartDetailsToRemove);
                _context.CartHeaders.Remove(cartHeader);

                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
