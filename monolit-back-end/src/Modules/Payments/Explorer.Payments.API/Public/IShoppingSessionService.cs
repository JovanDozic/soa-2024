using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IShoppingSessionService
    {
        Result<ShoppingSessionDto> StartSession(long userId);
        Result<ShoppingSessionDto> AddEvent(ShoppingEventDto eventDto, long userId);
        Result<ShoppingSessionDto> CloseSession(long userId);
    }
}
