using Explorer.Encounters.Core.Domain.SolvingStrategies;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IMiscEncounterRepository
    {
        MiscEncounter Get(long id);
        List<MiscEncounter> GetAll();
        MiscEncounter Create(MiscEncounter encounter);
        MiscEncounter Update(MiscEncounter encounter);
    }
}
