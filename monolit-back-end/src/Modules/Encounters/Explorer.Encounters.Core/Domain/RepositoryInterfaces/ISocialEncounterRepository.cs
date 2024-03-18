using Explorer.Encounters.Core.Domain.SolvingStrategies;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface ISocialEncounterRepository
    {
        SocialEncounter Get(long id);
        List<SocialEncounter> GetAll();
        SocialEncounter Create(SocialEncounter encounter);
        SocialEncounter Update(SocialEncounter encounter);
    }
}
