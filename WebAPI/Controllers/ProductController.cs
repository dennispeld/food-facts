using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFoodFactsService _foodFactsService;

        public ProductController()
        {
            var services = new ServiceCollection();
            services.LoadServices();
            var serviceProvider = services.BuildServiceProvider();

            _foodFactsService = serviceProvider.GetRequiredService<IFoodFactsService>();
        }

        /// <summary>
        /// Search of products by ingredient without specifying the number of products to retrieve,
        /// which will use the default limit
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [HttpGet("/search/ingredient/{ingredient}")]
        public async Task<IActionResult> SearchAction(string ingredient)
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync(ingredient).ConfigureAwait(false);

            if (!products.Any()) {
                return NotFound(new { ingredient });
            }

            return Ok(new { products });
        }

        /// <summary>
        /// Search of products by ingredient and the number of products to retrieve
        /// </summary>
        /// <param name="ingredient"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("/search/ingredient/{ingredient}/limit/{limit}")]
        public async Task<IActionResult> SearchAction(string ingredient, int limit)
        {
            var products = await _foodFactsService.GetProductsByIngredientAsync(ingredient, limit).ConfigureAwait(false);

            if (!products.Any()) {
                return NotFound(new { ingredient });
            }

            return Ok(new { products });
        }
    }
}