using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class OrderRepository : CrudDatabaseRepository<ShoppingCart, PaymentsContext>, IOrderRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<ShoppingCart> _dbSet;
        public OrderRepository(PaymentsContext dbContext) : base(dbContext) 
        {
            _context = dbContext;
            _dbSet = DbContext.Set<ShoppingCart>();
        }

        public Result<ShoppingCart> AddToCart(OrderItem orderItem, int userId)
        {
            var cart = _dbSet.Where(x => x.IdUser ==  userId).FirstOrDefault();
            ApplyDiscount(orderItem, orderItem.CouponCode);
            if (cart == null)
            {
                var orderItems = new List<OrderItem> { orderItem };
                var newCart = new ShoppingCart(userId, orderItems);
                _dbSet.Add(newCart);
                DbContext.SaveChanges();
                return newCart;
            }
            cart.Items.Add(orderItem);
            
            DbContext.Update(cart);
            DbContext.SaveChanges();
            return cart;
        }
        private void ApplyDiscount(OrderItem orderItem, string couponCode)
        {
            if (!string.IsNullOrEmpty(couponCode))
            {
                var coupon = DbContext.Coupons.FirstOrDefault(c => c.Code == couponCode);

                if (coupon != null)
                {
                    orderItem.Price -= (orderItem.Price * coupon.Discount / 100);
                }
                else
                {
                    // Handle the case where the coupon code doesn't match the one associated with the order item
                    // You can throw an exception, log a message, or handle it in any other way
                }
            }
            else
            {
                // Handle the case where either orderItem is null or couponCode is empty
                // You can throw an exception, log a message, or handle it in any other way
            }
        }


        public Result<ShoppingCart>? GetByUserId(int userId)
        {
            var cart = _dbSet.Where(i => i.IdUser == userId).FirstOrDefault();

            if(cart == null)
            {
                var orderItems=new List<OrderItem>();
                var newCart = new ShoppingCart(userId, orderItems);
                _dbSet.Add(newCart);
                DbContext.SaveChanges();
                return newCart;
            }
            return cart;
        }
    }
}
