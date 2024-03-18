using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Enums
{
    public class ShoppingEventEnums
    {
        public enum EventType
        {
            OpenSession,
            AddTourToCart,
            RemoveTourFromCart,
            AddBundleToCart,
            RemoveBundleFromCart,
            CloseSession,
            ExpiredSession,
        }
    }
}
