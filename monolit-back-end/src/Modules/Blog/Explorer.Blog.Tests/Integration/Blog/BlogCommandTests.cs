using Explorer.API.Controllers.Community;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using static Explorer.Blog.API.Enums.BlogEnums;

namespace Explorer.Blog.Tests.Integration.Blog
{
    [Collection("Sequential")]
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            /*// Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new API.Dtos.BlogDto
            {
                Id = 1,
                Title = "Srbija do Tokija",
                UserId = 1,
                Description = "Blog posvećen Srbiji!",
                CreationDate = DateTime.Now,
                Images = new string[]
                {
                    "image1.png",
                    "image2.png"
                },
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntites = dbContext.Blogs.ToList();
            storedEntites.ForEach(x =>  x.FromJson());
            var storedEntity = storedEntites.FirstOrDefault(x => x.Title == newEntity.Title);
            
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);*/
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new API.Dtos.BlogDto
            {
                UserId = -1,
                Description = "Test",
                CreationDate = DateTime.Now,
                Images = new string[] { },
            };

           // var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
          //  result.ShouldNotBeNull();
           // result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Publishes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var result = ((ObjectResult)controller.Publish(2).Result)?.Value as BlogDto;

            //Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe("2");
            result.Status.ShouldBe(BlogStatus.Published);

            //    Assert - Database
            var storedEntites = dbContext.Blogs.ToList();
            var storedEntity = storedEntites.FirstOrDefault(x => x.Id == 2);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new API.Dtos.BlogDto
            {
                Id = "2",
                Title = "Update test",
                UserId = 1,
                Description = "Update test",
                CreationDate = DateTime.Now,
                Images = Array.Empty<string>(),
            };

            //Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogDto;

            //Asert
            result.ShouldNotBeNull();
            result.Id.ShouldBe("2");
            result.Title.ShouldBe("Update test");
            result.Description.ShouldBe("Update test");
            result.Status.ShouldBe(updatedEntity.Status);
            result.UserId.ShouldBe(updatedEntity.UserId);

            //    Assert - Database
            var storedEntites = dbContext.Blogs.ToList();
            storedEntites.ForEach(x => x.FromJson());
            var storedEntity = storedEntites.FirstOrDefault(i => i.Title == updatedEntity.Title);

            storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            //Arange 
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                Id = "2",
                Description = "From update test"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Blogs.FirstOrDefault(i => i.Id == 2);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
