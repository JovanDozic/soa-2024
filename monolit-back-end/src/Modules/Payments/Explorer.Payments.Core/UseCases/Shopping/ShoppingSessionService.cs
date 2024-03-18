using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Session;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class ShoppingSessionService : CrudService<ShoppingSessionDto, ShoppingSession>, IShoppingSessionService
    {
        private readonly IShoppingSessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        public ShoppingSessionService(ICrudRepository<ShoppingSession> crudRepository, IMapper mapper, IShoppingSessionRepository sessionRepository) : base(crudRepository, mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public Result<ShoppingSessionDto> AddEvent(ShoppingEventDto eventDto, long userId)
        {
            var session = _sessionRepository.GetActivetByUserId(userId);
            if(session != null)
            {
                eventDto.Timestamp = DateTime.UtcNow;
                session.AddEvent(_mapper.Map<ShoppingEvent>(eventDto));
                session = CrudRepository.Update(session);
                return MapToDto(session);
            }
            return Result.Fail(FailureCode.NotFound);
        }

        public Result<ShoppingSessionDto> CloseSession(long userId)
        {
            var session = _sessionRepository.GetActivetByUserId(userId);
            if (session != null)
            {
                var closeSessionEvent = new ShoppingEvent(API.Enums.ShoppingEventEnums.EventType.CloseSession, DateTime.UtcNow);
                session.CloseSession(closeSessionEvent);
                session = CrudRepository.Update(session);
                return MapToDto(session);
            }
            return Result.Fail(FailureCode.NotFound);
        }

        public Result<ShoppingSessionDto> StartSession(long userId)
        {
            var events = new List<ShoppingEvent>();

            var session = _sessionRepository.GetActivetByUserId(userId);
            if(session != null)
            {
                if (session.Events.Max(e => e.Timestamp).AddMinutes(30) > DateTime.UtcNow)
                {
                    return MapToDto(session);
                }
                else
                {
                    CloseSession(userId);
                    var expiredSessionEvent = new ShoppingEvent(API.Enums.ShoppingEventEnums.EventType.ExpiredSession, session.Id, DateTime.UtcNow);
                    events.Add(expiredSessionEvent);
                }
            }
            
            var openSessionEvent = new ShoppingEvent(API.Enums.ShoppingEventEnums.EventType.OpenSession, DateTime.UtcNow);
            events.Add(openSessionEvent);
            var newSession = new ShoppingSession(userId,events,true);
            newSession = CrudRepository.Create(newSession);
            return MapToDto(newSession);
        }
    }
}
