using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class MiscEncounterRepository: IMiscEncounterRepository
    {

        private readonly EncountersContext _encountersContext;

        public MiscEncounterRepository(EncountersContext encountersContext)
        {
            _encountersContext = encountersContext;
        }

        public MiscEncounter Get(long id)
        {
            var encounter = _encountersContext.MiscEncounters.Find(id);
            if (encounter == null) throw new KeyNotFoundException("Encounter not found.");
            return encounter;
        }

        public List<MiscEncounter> GetAll()
        {
            return _encountersContext.MiscEncounters.ToList();
        }

        public MiscEncounter Create(MiscEncounter encounter)
        {
            _encountersContext.MiscEncounters.Add(encounter);
            _encountersContext.SaveChanges();
            return encounter;
        }

        public MiscEncounter Update(MiscEncounter encounter)
        {
            try
            {
                _encountersContext.Update(encounter);
                _encountersContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounter;
        }
    }
}
