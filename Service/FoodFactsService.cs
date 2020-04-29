using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// Service that is consuming OpenFoodFacts API
    /// </summary>
    public class FoodFactsService : IFoodFactsService
    {
        public Task<List<Product>> GetProductsByIngredientAsync(string ingredient, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
