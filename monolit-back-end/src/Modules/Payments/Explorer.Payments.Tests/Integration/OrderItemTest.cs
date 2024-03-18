using Explorer.API.Controllers.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Tests;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

[Collection("Sequential")]
public class MyOrderCommandTests : BasePaymentsIntegrationTest
{
    public MyOrderCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void AddToCart_ShouldAddItem_WithValidData()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var orderController = CreateOrderController(scope);

        // Set up initial conditions with valid data
        var orderItemDto = new OrderItemDto
        {
            IdType = 1,
            Name = "Sample Tour",
            Price = 29.99,
            Image = "sample-image.jpg",
            CouponCode = "SAMPLE123"
            // Add other necessary data
        };

        // Act
        var result = orderController.AddToCart(orderItemDto, 1); // Pass the correct userId

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ActionResult<ShoppingCartDto>>();// Assuming a successful result is OkObjectResult
    }

    

        private static OrderController CreateOrderController(IServiceScope scope)
    {
        return new OrderController(scope.ServiceProvider.GetRequiredService<IOrderService>())
        {
            // If you're not using authentication, you can leave ControllerContext empty
        };
    }
}
