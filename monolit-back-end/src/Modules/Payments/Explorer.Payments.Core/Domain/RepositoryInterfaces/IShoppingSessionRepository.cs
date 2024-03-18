using Explorer.Payments.Core.Domain.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingSessionRepository
    {
        ShoppingSession GetActivetByUserId(long userId);
    }
}
