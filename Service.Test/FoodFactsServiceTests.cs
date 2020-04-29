using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Service.Test
{
    public class FoodFactsServiceTests
    {
        private readonly IFoodFactsService _foodFactsService;

        public FoodFactsServiceTests()
        {
            var services = new ServiceCollection();
            services.LoadServices();
            var serviceProvider = services.BuildServiceProvider();

            _foodFactsService = serviceProvider.GetRequiredService<IFoodFactsService>();
        }

        [Fact]
        public async Task GetProducts_ByIngredient_NotEmpty()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("egg", 5).ConfigureAwait(false);

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProducts_ByIngredient_LimitedResults()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("chocolate", 10).ConfigureAwait(false);

            Assert.Equal(10, products.Count);
        }

        [Fact]
        public async Task GetProducts_ByIngredient_DefaultLimit()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("sugar").ConfigureAwait(false);

            Assert.Equal(20, products.Count);
        }
    }
}
