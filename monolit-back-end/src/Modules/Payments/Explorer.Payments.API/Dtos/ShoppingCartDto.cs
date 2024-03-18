namespace Explorer.Payments.API.Dtos
{
    public class ShoppingCartDto
    {
         public long Id { get; set; }
         public int IdUser { get; init; }
         public List<OrderItemDto> Items { get; init; }
    }
}
