using Explorer.API.Controllers.Encounter;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Tests.Integration.Encounter
{
    [Collection("Sequential")]
    public class HiddenEncounterCommandTest : BaseEncounterIntegrationTest
    {
        public HiddenEncounterCommandTest(EncounterTestFactory factory) : base(factory) { }

        [Fact]
        public void Solves_hidden()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var encounterId = -5;
            var personLocation = new ParticipantLocationDto
            {
                Username = "participant11",
                Latitude = 45.244442,
                Longitude = 19.841521
            };

            // Act
            var result = ((ObjectResult)controller.SolveHidden(encounterId, personLocation).Result)?.Value as HiddenEncounterDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Participants.Count.ShouldBe(1);
            result.Completers.Count.ShouldBe(1);

            // Assert - Database
        }

        [Fact]
        public void Solves_hidden_fails_out_of_range()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var encounterId = -6;
            var personLocation = new ParticipantLocationDto
            {
                Username = "participant12",
                Latitude = 45.768442,
                Longitude = 19.889521
            };

            // Act
            var result = ((ObjectResult)controller.SolveHidden(encounterId, personLocation).Result)?.Value as HiddenEncounterDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Participants.Count.ShouldBe(2);
            result.Completers.Count.ShouldBe(0);

            // Assert - Database
        }

        [Fact]
        public void Solves_hidden_fails_out_of_point_range()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var encounterController = CreateEncounterController(scope);
            var encounterId = -7;
            var personLocation = new ParticipantLocationDto
            {
                Username = "participant13",
                Latitude = 35.244442,
                Longitude = 29.841521
            };

            // Act
            var activatedResult = ((ObjectResult)encounterController.Activate(encounterId, personLocation).Result)?.Value as EncounterDto;
            var result = ((ObjectResult)controller.SolveHidden(encounterId, personLocation).Result)?.Value as HiddenEncounterDto;
            
            // Assert - Response
            result.ShouldNotBeNull();
            activatedResult.ShouldNotBeNull();

            activatedResult.Participants.Count.ShouldBe(2);

            result.Participants.Count.ShouldBe(2);
            result.Completers.Count.ShouldBe(0);
            var participant = result.Participants.FirstOrDefault(p => p.Username == "participant13");
            participant.ShouldNotBeNull();

            // Assert - Database
        }

        private static HiddenEncounterController CreateController(IServiceScope scope)
        {
            return new HiddenEncounterController(scope.ServiceProvider.GetRequiredService<IHiddenEncounterService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        private static EncounterController CreateEncounterController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
