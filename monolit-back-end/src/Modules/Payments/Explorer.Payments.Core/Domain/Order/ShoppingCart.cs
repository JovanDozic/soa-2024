using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.Order
{
    public class ShoppingCart : Entity
    {
        public int IdUser { get; init; }
        public List<OrderItem> Items { get; init; }
        public ShoppingCart(int idUser, List<OrderItem> items)
        {
            IdUser = idUser;
            Items = items;
            Validate();
        }
        private void Validate()
        {
            if (IdUser == 0) throw new ArgumentOutOfRangeException("User doesn't exist!");
        }

    }
}
