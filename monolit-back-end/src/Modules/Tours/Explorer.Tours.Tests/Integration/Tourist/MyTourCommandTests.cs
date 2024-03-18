using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Tour;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.Tests.Integration.Tourist;

[Collection("Sequential")]
public class MyTourCommandTests : BaseToursIntegrationTest
{
    public MyTourCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourDto
        {
            Name = "Katarinina tura",
            Description = "Obilazimo muzeje i parkove.",
            Difficult = 3,
            Status = TourStatus.Published,
            Price = 49.99,
            Points = new List<PointDto>(), 
            Tags = new List<TagDto>(), 
            RequiredTimes = new List<RequiredTimeDto>(), 
            Reviews = new List<TourReviewDto>(), 
            //Guide = null, 
            AuthorId = 1, 
            Length = 10.5f, 
            PublishTime = DateTime.Now, 
            ArhiveTime = null, 
            Problems = new List<ProblemDto>(),
            MyOwn = true
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database

    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourDto
        {
            Description = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
    }


    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}