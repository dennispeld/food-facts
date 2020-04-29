using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Service.Test
{
    public class FoodFactsServiceTests
    {
        private readonly IFoodFactsService _foodFactsService;

        /// <summary>
        /// Setting up the service that will be used in each test
        /// </summary>
        public FoodFactsServiceTests()
        {
            var services = new ServiceCollection();
            services.LoadServices();
            var serviceProvider = services.BuildServiceProvider();

            _foodFactsService = serviceProvider.GetRequiredService<IFoodFactsService>();
        }

        /// <summary>
        /// Should retrieve products containing "egg" within the ingredients
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProducts_ByIngredient_NotEmpty()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("egg", 5).ConfigureAwait(false);

            Assert.NotEmpty(products);
        }

        /// <summary>
        /// Should retrieve 10 products containing "chocolate" within the ingredients
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProducts_ByIngredient_LimitedResults()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("chocolate", 10).ConfigureAwait(false);

            Assert.Equal(10, products.Count);
        }

        /// <summary>
        /// Should retrieve the default amount (20) of products containing "sugar" within the ingredients
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProducts_ByIngredient_DefaultLimit()
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync("sugar").ConfigureAwait(false);

            Assert.Equal(20, products.Count);
        }
    }
}
