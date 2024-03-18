using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class SocialEncounterRepository: ISocialEncounterRepository
    {
        private readonly EncountersContext _encountersContext;

        public SocialEncounterRepository(EncountersContext encountersContext)
        {
            _encountersContext = encountersContext;
        }

        public SocialEncounter Get(long id)
        {
            var encounter = _encountersContext.SocialEncounters.Find(id);
            if (encounter == null) throw new KeyNotFoundException("Encounter not found.");
            return encounter;
        }

        public List<SocialEncounter> GetAll()
        {
            return _encountersContext.SocialEncounters.ToList();
        }

        public SocialEncounter Create(SocialEncounter encounter)
        {
            _encountersContext.SocialEncounters.Add(encounter);
            _encountersContext.SaveChanges();
            return encounter;
        }

        public SocialEncounter Update(SocialEncounter encounter)
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
