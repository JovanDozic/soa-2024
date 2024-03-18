using Explorer.Encounters.Core.Domain.SolvingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IHiddenEncounterRepository
    {
        HiddenEncounter Get(long id);
        List<HiddenEncounter> GetAll();
        HiddenEncounter Create(HiddenEncounter encounter);
        HiddenEncounter Update(HiddenEncounter encounter);
    }
}
