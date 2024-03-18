using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class HiddenEncounterRepository : IHiddenEncounterRepository
    {
        private readonly EncountersContext _encountersContext;

        public HiddenEncounterRepository(EncountersContext encountersContext)
        {
            _encountersContext = encountersContext;
        }

        public HiddenEncounter Create(HiddenEncounter encounter)
        {
            _encountersContext.HiddenEncounters.Add(encounter);
            _encountersContext.SaveChanges();
            return encounter;
        }

        public HiddenEncounter Get(long id)
        {
            var encounter = _encountersContext.HiddenEncounters.Find(id);
            if (encounter == null) throw new KeyNotFoundException("Encounter not found.");
            return encounter;
        }

        public List<HiddenEncounter> GetAll()
        {
            return _encountersContext.HiddenEncounters.ToList();
        }

        public HiddenEncounter Update(HiddenEncounter encounter)
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
