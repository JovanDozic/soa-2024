//using Explorer.API.Controllers.Author.Tour;
//using Explorer.Tours.API.Dtos.Tours;
//using Explorer.Tours.API.Public.Administration;
//using Explorer.Tours.Infrastructure.Database;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Shouldly;

//namespace Explorer.Tours.Tests.Integration.Author
//{
//    public class TourCommandTests : BaseToursIntegrationTest
//    {
//        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

//        [Fact]
//        public void Creates()
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var controller = CreateController(scope);
//            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
//            var newEntity = new TourDto
//            {
//                Name = "From test",
//                Description = "Tour from test",
//                Difficult = 1,
//                Tags = "city,people",
//                Status = "Draft",
//                Price = 0,
//                AuthorId = 1
//            };

//            // Act
//            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourIn;

//            // Assert - Response
//            result.ShouldNotBeNull();
//            result.Id.ShouldNotBe(0);
//            result.Name.ShouldBe(newEntity.Name);

//            // Assert - Database
//            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
//            storedEntity.ShouldNotBeNull();
//            storedEntity.Id.ShouldBe(result.Id);
//        }

//        [Fact]
//        public void Create_fails_invalid_data()
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var controller = CreateController(scope);
//            var updatedEntity = new TourIn
//            {
//                Description = "Test"
//            };

//            // Act
//            var result = (ObjectResult)controller.Create(updatedEntity).Result;

//            // Assert
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(400);
//        }

//        //[Fact]
//        //public void Updates()
//        //{
//        //    Arrange
//        //    using var scope = Factory.Services.CreateScope();
//        //    var controller = CreateController(scope);
//        //    var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
//        //    var updatedEntity = new TourIn
//        //    {
//        //        Id = -1,
//        //        Name = "From update test",
//        //        Description = "Update test",
//        //        Difficult = 2,
//        //        Tags = "city",
//        //        Status = "Draft",
//        //        Price = 0,
//        //        AuthorId = 1
//        //    };

//        //    Act
//        //   var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourIn;

//        //    Assert - Response
//        //    result.ShouldNotBeNull();
//        //    result.Id.ShouldBe(-1);
//        //    result.Name.ShouldBe(updatedEntity.Name);
//        //    result.Description.ShouldBe(updatedEntity.Description);
//        //    result.Difficult.ShouldBe(updatedEntity.Difficult);
//        //    result.Tags.ShouldBe(updatedEntity.Tags);
//        //    result.Status.ShouldBe(updatedEntity.Status);
//        //    result.Price.ShouldBe(updatedEntity.Price);
//        //    result.AuthorId.ShouldBe(updatedEntity.AuthorId);

//        //    Assert - Database
//        //    var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "From update test");
//        //    storedEntity.ShouldNotBeNull();
//        //    storedEntity.Description.ShouldBe(updatedEntity.Description);
//        //    var oldEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "From test");
//        //    oldEntity.ShouldBeNull();
//        //}

//        [Fact]
//        public void Update_fails_invalid_id()
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var controller = CreateController(scope);
//            var updatedEntity = new TourIn
//            {
//                Id = -1000,
//                Name = "From update test",
//                Description = "Update test",
//                Difficult = 2,
//                Tags = "city",
//                Status = "Draft",
//                Price = 0,
//                AuthorId = 1
//            };

//            // Act
//            var result = (ObjectResult)controller.Update(updatedEntity).Result;

//            // Assert
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(404);
//        }

//        [Fact]
//        public void Deletes()
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var controller = CreateController(scope);
//            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

//            // Act
//            var result = (OkResult)controller.Delete(-1);

//            // Assert - Response
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(200);

//            // Assert - Database
//            var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == 1);
//            storedCourse.ShouldBeNull();
//        }

//        [Fact]
//        public void Delete_fails_invalid_id()
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var controller = CreateController(scope);

//            // Act
//            var result = (ObjectResult)controller.Delete(-1000);

//            // Assert
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(404);
//        }

//        private static TourController CreateController(IServiceScope scope)
//        {
//            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
//            {
//                ControllerContext = BuildContext("-1")
//            };
//        }

//    }

//}
