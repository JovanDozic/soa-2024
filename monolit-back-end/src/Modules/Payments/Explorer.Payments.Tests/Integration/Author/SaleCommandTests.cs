using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using Explorer.Payments.Tests;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos.Tours;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class SaleCommandTests : BasePaymentsIntegrationTest
    {
        public SaleCommandTests(PaymentsTestFactory factory) : base(factory) { }
        
        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var sale = new SaleDto()
            {
                SaleStart = DateTime.UtcNow,
                SaleEnd = DateTime.UtcNow.AddDays(7),
                DiscountPercentage = 10,
                IsActive = true,
                ToursOnSale = new List<TourDto>
                { }
            };


            var result = ((ObjectResult)controller.Create(sale).Result)?.Value as SaleDto;

            result.ShouldNotBeNull();
        } 
        // ... (Similar modifications for other test methods)

        private static SaleController CreateController(IServiceScope scope)
        {
            return new SaleController(scope.ServiceProvider.GetRequiredService<ISaleService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
