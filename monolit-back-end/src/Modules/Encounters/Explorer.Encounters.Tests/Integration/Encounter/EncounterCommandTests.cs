using Explorer.API.Controllers.Encounter;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Encounters.Tests.Integration.Encounter
{
    [Collection("Sequential")]
    public class EncounterCommandTests: BaseEncounterIntegrationTest
    {
        public EncounterCommandTests(EncounterTestFactory factory) : base(factory){}
        
           [Fact]
           public void Activates()
           {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);
                var encounterId = -1;
                var personLocation = new ParticipantLocationDto
                {
                    Username = "turista1",
                    Latitude = 45.243952,
                    Longitude = 19.841275
                };
            
                // Act
                var result = ((ObjectResult)controller.Activate(encounterId, personLocation).Result)?.Value as EncounterDto;

                // Assert - Response
                result.ShouldNotBeNull();

                // Assert - Database
           }

           [Fact]
           public void Activates_fails_out_of_range()
           {
               // Arrange
               using var scope = Factory.Services.CreateScope();
               var controller = CreateController(scope);
               var encounterId = -1;
               var personLocation = new ParticipantLocationDto
               {
                   Username = "turista1",
                   Latitude = 45.241695,
                   Longitude = 19.842555
               };

               // Act
               var result = ((ObjectResult)controller.Activate(encounterId, personLocation).Result)?.Value as EncounterDto;

               // Assert - Response
               result.ShouldNotBeNull();

               // Assert - Database
           }

           [Fact]
           public void Activates_fails_already_activated()
           {
               // Arrange
               using var scope = Factory.Services.CreateScope();
               var controller = CreateController(scope);
               var encounterId = -2;
               var personLocation = new ParticipantLocationDto
               {
                   Username = "turista1",
                   Latitude = 45.244230,
                   Longitude = 19.841414
               };

               // Act
               var result = ((ObjectResult)controller.Activate(encounterId, personLocation).Result)?.Value as EncounterDto;

               // Assert - Response
               result.ShouldNotBeNull();

               // Assert - Database
           }

           
        private static EncounterController CreateController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
