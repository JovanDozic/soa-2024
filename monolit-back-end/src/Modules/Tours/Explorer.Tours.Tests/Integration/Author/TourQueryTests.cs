using Explorer.API.Controllers.Author.Tour;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Author
{
    public class TourQueryTests : BaseToursIntegrationTest
    {
        public TourQueryTests(ToursTestFactory factory) : base(factory) { }

        //[Fact]
        //public void Retrieves_all()
        //{
        //    // Arrange
        //    using var scope = Factory.Services.CreateScope();
        //    var controller = CreateController(scope);

        //    // Act
        //    var result = ((ObjectResult)controller.GetAll(1, int.MaxValue).Result)?.Value as PagedResult<TourIn>;

        //    // Assert
        //    result.ShouldNotBeNull();
        //    result.Results.Count.ShouldBe(3);
        //    result.TotalCount.ShouldBe(3);
        //}

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
