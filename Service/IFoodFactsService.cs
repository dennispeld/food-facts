using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFoodFactsService
    {
        Task<List<Product>> GetProductsByIngredientAsync(string ingredient, int limit);
    }
}
