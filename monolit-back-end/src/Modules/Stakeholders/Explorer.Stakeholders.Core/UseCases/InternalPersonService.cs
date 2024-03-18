using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalPersonService: IInternalPersonService
    {
        private readonly IPersonRepository _personRepository;

        public InternalPersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Result RewardWithXp(List<string> usernames, int xp)
        {
            var persons = _personRepository.GetAll(usernames);
            if(persons.Count < 1) return Result.Fail(FailureCode.NotFound);
            persons.ForEach(person =>
            {
                person.GainXP(xp);
                _personRepository.Update(person);
            });
            return Result.Ok();
        }
    }
}
