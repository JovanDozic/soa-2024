using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class OrderService : CrudService<ShoppingCartDto, ShoppingCart>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(ICrudRepository<ShoppingCart> crudRepository, IMapper mapper, IOrderRepository orderRepository) : base(crudRepository, mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public Result<ShoppingCartDto> AddToCart(OrderItemDto orderItem, int userId)
        {
            var cart = _orderRepository.AddToCart(_mapper.Map<OrderItem>(orderItem), userId);
            return MapToDto(cart.Value);
        }

        public Result<ShoppingCartDto>? GetByIdUser(int id)
        {
            var shoppingCart = _orderRepository.GetByUserId(id);
            return MapToDto(shoppingCart.Value);
        }
    }
}
