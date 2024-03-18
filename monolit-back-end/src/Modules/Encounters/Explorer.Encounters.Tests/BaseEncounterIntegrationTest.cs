using Explorer.BuildingBlocks.Tests;

namespace Explorer.Encounters.Tests
{
    public class BaseEncounterIntegrationTest: BaseWebIntegrationTest<EncounterTestFactory>
    {
        public BaseEncounterIntegrationTest(EncounterTestFactory factory) : base(factory)
        {
        }
    }
}
