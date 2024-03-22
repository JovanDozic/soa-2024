using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    public class ClubTests : BaseToursIntegrationTest
    {
        public ClubTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ClubRegistrationDto
            {
                Name = "From test",
                Description = "Tour from test",
                URL = "urlImage",
                OwnerId = 1
            };

            // Act
            var result = ((ObjectResult)controller.CreateAsync(newEntity).Result)?.Value as ClubRegistrationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            // TODO: fix
            //var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newEntity.Name);
            //storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubRegistrationDto
            {
                Description = "Test"
            };

            // Act
            var result = (ObjectResult)controller.CreateAsync(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubRegistrationDto
            {
                Id = -1000,
                Name = "From update test",
                Description = "Update test",
                URL = "imageUrl",
                OwnerId = 1
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity, updatedEntity.Id).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }
        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
