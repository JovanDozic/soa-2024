using Explorer.API.Controllers.Encounter;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Encounters.Tests.Integration.Encounter
{
    [Collection("Sequential")]
    public class SocialEncounterCommandTests: BaseEncounterIntegrationTest
    {
        public SocialEncounterCommandTests(EncounterTestFactory factory) : base(factory) {}

        [Fact]
            public void Solves_social()
            {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);
                var encounterId = -4;
                var personLocation = new ParticipantLocationDto
                {
                    Username = "turista3@gmail.com",
                    Latitude = 44.653797,
                    Longitude = 21.147734
                };

                // Act
                var result = ((ObjectResult)controller.SolveSocial(encounterId, personLocation).Result)?.Value as SocialEncounterDto;

                // Assert - Response
                result.ShouldNotBeNull();
                result.Participants.Count.ShouldBe(0);
                
                // Assert - Database
            }
            [Fact]
            public void Solves_social_fails_out_of_range()
            {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);
                var encounterId = -3;
                var personLocation = new ParticipantLocationDto
                {
                    Username = "turista3@gmail.com",
                    Latitude = 37.970612,
                    Longitude = 23.724505
                };

                // Act
                var result = ((ObjectResult)controller.SolveSocial(encounterId, personLocation).Result)?.Value as SocialEncounterDto;

                // Assert - Response
                result.ShouldNotBeNull();
                result.Participants.Count.ShouldBe(3);

                // Assert - Database
            }

            [Fact]
            public void Solves_social_fails_in_range()
            {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);
                var encounterId = -3;
                var personLocation = new ParticipantLocationDto
                {
                    Username = "turista2@gmail.com",
                    Latitude = 37.971375,
                    Longitude = 23.726168
                };

                // Act
                var result = ((ObjectResult)controller.SolveSocial(encounterId, personLocation).Result)?.Value as SocialEncounterDto;

                // Assert - Response
                result.ShouldNotBeNull();
            
                // Assert - Database
            }

            private static SocialEncounterController CreateController(IServiceScope scope)
            {
                return new SocialEncounterController(scope.ServiceProvider.GetRequiredService<ISocialEncounterService>())
                {
                    ControllerContext = BuildContext("-1")
                };
            }
    }
}
