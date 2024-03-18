using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Blog.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Dtos;
using Shouldly;
using Explorer.API.Controllers.Author.Tour;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Tests.Integration.Campaign;
[Collection("Sequential")]
public class CampaignTests : BaseToursIntegrationTest
{
    public CampaignTests(ToursTestFactory factory) : base(factory) { }
    [Fact]
    public void Creates()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new API.Dtos.CampaignDto
        {
            Id = 104,
            Title = "Title",
            Tours = new List<TourDto>
            {
                new TourDto
                {
                    Id = 10,
                    Name = "Excursion to Spain",
                    Description = "A fantastic journey through the landscapes of Spain",
                    Difficult = 3,
                    Status = API.Enums.TourEnums.TourStatus.Draft,
                    Price = 250,
                    Points = new List<PointDto>
                    {
                        new PointDto
                        {
                            Name = "Madrid Central Plaza",
                            Description = "A bustling city center with historical significance",
                            Latitude = 40.416775,
                            Longitude = -3.70379,
                            Picture = "madrid_plaza.jpg",
                            Public = true
                        }
                    },
                    Tags = new List<TagDto>
                    {
                        new TagDto { Name = "Adventure" },
                        new TagDto { Name = "Historical" }
                    },
                    RequiredTimes = new List<RequiredTimeDto>
                    {
                        new RequiredTimeDto
                        {
                            TransportType = API.Enums.TourEnums.TransportType.Walk,
                            Minutes = 45
                        }
                    },
                    Reviews = new List<TourReviewDto>
                    {
                        new TourReviewDto
                        {
                            Rating = 5,
                            Comment = "An unforgettable experience, highly recommended!",
                            TouristId = 101,
                            TouristUsername = "traveler_jane",
                            TourDate = Convert.ToDateTime("2023-11-15T10:00:00.000Z"),
                            CreationDate = Convert.ToDateTime("2023-11-20T09:00:00.000Z"),
                            Images = new List<string> { "review1.jpg" }
                        }
                    },
                    ////Guide = new GuideDto
                    //{
                    //    Id = 22,
                    //    Name = "Carlos",
                    //    Surname = "Gomez",
                    //    Email = "carlos.gomez@example.com"
                    //},
                    AuthorId = 34,
                    Length = 150,
                    PublishTime = Convert.ToDateTime("2023-10-01T08:00:00.000Z"),
                    ArhiveTime = Convert.ToDateTime("2024-10-01T08:00:00.000Z"),
                    Problems = new List<ProblemDto>
                    {
                        new ProblemDto
                        {
                            Id = 10,
                            Category = "Logistics",
                            Priority = false,
                            Description = "Delay in transportation schedule",
                            Time = Convert.ToDateTime("2023-11-12T07:30:00.000Z"),
                            TourId = 1,
                            TouristId = 205,
                            AuthorsSolution = "Provided alternate transportation",
                            IsSolved = true,
                            UnsolvedProblemComment = "",
                            Deadline = Convert.ToDateTime("2023-11-13T20:00:00.000Z")
                        }
                    }
                },
                new TourDto
                {
                    Id = 20,
                    Name = "Alpine Adventure",
                    Tags = new List<TagDto>
                    {
                        new TagDto { Name = "Nature" },
                        new TagDto { Name = "Hiking" }
                    },
                    //Guide = new GuideDto
                    //{
                    //    Id = 45,
                    //    Name = "Helena",
                    //    Email = "helena.berger@example.com",
                    //    Surname = "Berger"
                    //},
                    Price = 350,
                    Length = 200,
                    Points = new List<PointDto>
                    {
                        new PointDto
                        {
                            Name = "Eagles Nest",
                            Public = true,
                            Picture = "eagles_nest.jpg",
                            Latitude = 47.516231,
                            Longitude = 14.550072,
                            Description = "Scenic viewpoint atop the Alpine ridge"
                        },
                        new PointDto
                        {
                            Name = "Alpine Lake",
                            Public = false,
                            Picture = "alpine_lake.jpg",
                            Latitude = 47.269212,
                            Longitude = 11.404102,
                            Description = "Crystal clear waters surrounded by mountains"
                        }
                    },
                    Status = API.Enums.TourEnums.TourStatus.Archived,
                    Reviews = new List<TourReviewDto>
                    {
                        new TourReviewDto
                        {
                            Images = new List<string> { "review_alpine.jpg" },
                            Rating = 4,
                            Comment = "Stunning views, but quite challenging. Be prepared!",
                            TourDate = Convert.ToDateTime("2023-08-05T12:00:00.000Z"),
                            TouristId = 202,
                            CreationDate = Convert.ToDateTime("2023-08-10T15:30:00.000Z"),
                            TouristUsername = "mountain_lover"
                        }
                    },
                    AuthorId = 56,
                    Problems = new List<ProblemDto>
                    {
                        new ProblemDto
                        {
                            Id = 20,
                            Time = Convert.ToDateTime("2023-08-02T16:45:00.000Z"),
                            TourId = 2,
                            Category = "Weather",
                            Deadline = Convert.ToDateTime("2023-08-04T18:00:00.000Z"),
                            IsSolved = true,
                            Priority = true,
                            TouristId = 303,
                            Description = "Unexpected snowstorm",
                            AuthorsSolution = "Rerouted to safer paths, provided extra gear",
                            UnsolvedProblemComment = ""
                        }
                    },
                    Difficult = 5,
                    ArhiveTime = Convert.ToDateTime("2024-06-01T09:30:00.000Z"),
                    Description = "Breathtaking hike through the Alpine trails",
                    PublishTime = Convert.ToDateTime("2023-06-01T09:30:00.000Z"),
                    RequiredTimes = new List<RequiredTimeDto>
                    {
                        new RequiredTimeDto
                        {
                            Minutes = 60,
                            TransportType = API.Enums.TourEnums.TransportType.Bicycle
                        }
                    }
                }
            },
            TouristId = 1,
        };

        //Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CampaignDto;

        //Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.TouristId.ShouldBe(newEntity.TouristId);
        // Assert - Database
        var storedEntites = dbContext.Campaigns.ToList();
        storedEntites.ForEach(x => x.FromJson());
        var storedEntity = storedEntites.FirstOrDefault(x => x.TouristId == newEntity.TouristId);

        storedEntity.ShouldNotBeNull();
        result.Tours.Count.ShouldBeGreaterThanOrEqualTo(2);

    }

    [Fact]
    public void GetAll_ReturnsAllEntities()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Assume Campaigns is a DbSet<CampaignDto> in ToursContext
        var expectedEntities = new List<CampaignDto>
        {
            new CampaignDto
            {
            Id = 1,
            Title = "Title",
            Tours = new List<TourDto>
            {
                new TourDto
                {
                    Id = 1,
                    Name = "Excursion to Spain",
                    Description = "A fantastic journey through the landscapes of Spain",
                    Difficult = 3,
                    Status = API.Enums.TourEnums.TourStatus.Draft,
                    Price = 250,
                    Points = new List<PointDto>
                    {
                        new PointDto
                        {
                            Name = "Madrid Central Plaza",
                            Description = "A bustling city center with historical significance",
                            Latitude = 40.416775,
                            Longitude = -3.70379,
                            Picture = "madrid_plaza.jpg",
                            Public = true
                        }
                    },
                    Tags = new List<TagDto>
                    {
                        new TagDto { Name = "Adventure" },
                        new TagDto { Name = "Historical" }
                    },
                    RequiredTimes = new List<RequiredTimeDto>
                    {
                        new RequiredTimeDto
                        {
                            TransportType = API.Enums.TourEnums.TransportType.Walk,
                            Minutes = 45
                        }
                    },
                    Reviews = new List<TourReviewDto>
                    {
                        new TourReviewDto
                        {
                            Rating = 5,
                            Comment = "An unforgettable experience, highly recommended!",
                            TouristId = 101,
                            TouristUsername = "traveler_jane",
                            TourDate = Convert.ToDateTime("2023-11-15T10:00:00.000Z"),
                            CreationDate = Convert.ToDateTime("2023-11-20T09:00:00.000Z"),
                            Images = new List<string> { "review1.jpg" }
                        }
                    },
                    //Guide = new GuideDto
                    //{
                    //    Id = 22,
                    //    Name = "Carlos",
                    //    Surname = "Gomez",
                    //    Email = "carlos.gomez@example.com"
                    //},
                    AuthorId = 34,
                    Length = 150,
                    PublishTime = Convert.ToDateTime("2023-10-01T08:00:00.000Z"),
                    ArhiveTime = Convert.ToDateTime("2024-10-01T08:00:00.000Z"),
                    Problems = new List<ProblemDto>
                    {
                        new ProblemDto
                        {
                            Id = 1,
                            Category = "Logistics",
                            Priority = false,
                            Description = "Delay in transportation schedule",
                            Time = Convert.ToDateTime("2023-11-12T07:30:00.000Z"),
                            TourId = 1,
                            TouristId = 205,
                            AuthorsSolution = "Provided alternate transportation",
                            IsSolved = true,
                            UnsolvedProblemComment = "",
                            Deadline = Convert.ToDateTime("2023-11-13T20:00:00.000Z")
                        }
                    }
                },
                new TourDto
                {
                    Id = 2,
                    Name = "Alpine Adventure",
                    Tags = new List<TagDto>
                    {
                        new TagDto { Name = "Nature" },
                        new TagDto { Name = "Hiking" }
                    },
                    //Guide = new GuideDto
                    //{
                    //    Id = 45,
                    //    Name = "Helena",
                    //    Email = "helena.berger@example.com",
                    //    Surname = "Berger"
                    //},
                    Price = 350,
                    Length = 200,
                    Points = new List<PointDto>
                    {
                        new PointDto
                        {
                            Name = "Eagles Nest",
                            Public = true,
                            Picture = "eagles_nest.jpg",
                            Latitude = 47.516231,
                            Longitude = 14.550072,
                            Description = "Scenic viewpoint atop the Alpine ridge"
                        },
                        new PointDto
                        {
                            Name = "Alpine Lake",
                            Public = false,
                            Picture = "alpine_lake.jpg",
                            Latitude = 47.269212,
                            Longitude = 11.404102,
                            Description = "Crystal clear waters surrounded by mountains"
                        }
                    },
                    Status = API.Enums.TourEnums.TourStatus.Archived,
                    Reviews = new List<TourReviewDto>
                    {
                        new TourReviewDto
                        {
                            Images = new List<string> { "review_alpine.jpg" },
                            Rating = 4,
                            Comment = "Stunning views, but quite challenging. Be prepared!",
                            TourDate = Convert.ToDateTime("2023-08-05T12:00:00.000Z"),
                            TouristId = 202,
                            CreationDate = Convert.ToDateTime("2023-08-10T15:30:00.000Z"),
                            TouristUsername = "mountain_lover"
                        }
                    },
                    AuthorId = 56,
                    Problems = new List<ProblemDto>
                    {
                        new ProblemDto
                        {
                            Id = 2,
                            Time = Convert.ToDateTime("2023-08-02T16:45:00.000Z"),
                            TourId = 2,
                            Category = "Weather",
                            Deadline = Convert.ToDateTime("2023-08-04T18:00:00.000Z"),
                            IsSolved = true,
                            Priority = true,
                            TouristId = 303,
                            Description = "Unexpected snowstorm",
                            AuthorsSolution = "Rerouted to safer paths, provided extra gear",
                            UnsolvedProblemComment = ""
                        }
                    },
                    Difficult = 5,
                    ArhiveTime = Convert.ToDateTime("2024-06-01T09:30:00.000Z"),
                    Description = "Breathtaking hike through the Alpine trails",
                    PublishTime = Convert.ToDateTime("2023-06-01T09:30:00.000Z"),
                    RequiredTimes = new List<RequiredTimeDto>
                    {
                        new RequiredTimeDto
                        {
                            Minutes = 60,
                            TransportType = API.Enums.TourEnums.TransportType.Bicycle
                        }
                    }
                }
            },
            TouristId = 1
            }
        };

        // Act
        var result = ((ObjectResult)controller.GetAll(1, int.MaxValue).Result)?.Value as List<CampaignDto>;

        // Assert
        Assert.NotNull(result);

    }
    private static CampaignController CreateController(IServiceScope scope)
    {
        return new CampaignController(scope.ServiceProvider.GetRequiredService<ICampaignService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
