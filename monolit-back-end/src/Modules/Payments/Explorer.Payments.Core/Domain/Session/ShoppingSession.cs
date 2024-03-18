using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.Session
{
    public class ShoppingSession : Entity
    {
        public long UserId { get; private set; }
        public List<ShoppingEvent> Events { get; private set; }
        public bool IsActive { get; private set; }

        public ShoppingSession(long userId, List<ShoppingEvent> events, bool isActive)
        {
            UserId = userId;
            Events = events;
            IsActive = isActive;
            Validate();

        }

        private void Validate()
        {
            if (UserId == 0) throw new Exception("User doesn't exist");
        }

        internal void CloseSession(ShoppingEvent closeEvent)
        {
            Events.Add(closeEvent);
            IsActive = false;
        }

        internal void AddEvent(ShoppingEvent @event)
        {
            Events.Add(@event);
        }
    }
}
