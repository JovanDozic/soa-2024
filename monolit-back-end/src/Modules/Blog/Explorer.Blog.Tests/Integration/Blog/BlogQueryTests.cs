using Explorer.API.Controllers.Community;
using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Public;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Blog.Tests.Integration.Blog
{
    [Collection("Sequential")]
    public class BlogQueryTests : BaseBlogIntegrationTest
    {
        public BlogQueryTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            //Arrange

            //Act

            //Assert
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
