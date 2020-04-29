using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;

namespace Service
{
    /// <summary>
    /// Service that is consuming OpenFoodFacts API
    /// </summary>
    public class FoodFactsService : IFoodFactsService
    {
        private const string searchBaseUrl = "https://us.openfoodfacts.org/cgi/search.pl";
        private readonly HttpClient _client;

        public FoodFactsService(HttpClient client) => _client = client;

        public async Task<List<Product>> GetProductsByIngredientAsync(string ingredient, int limit = 20)
        {
            // build a search URL and send GET request
            var httpResponse = await _client
                .GetAsync(Url.Combine(
                    searchBaseUrl,
                    "?action=process",
                    $"&tagtype_0=ingredients&tag_contains_0=contains&tag_0={ingredient}",
                    $"&page_size={limit}",
                    "&json=true"))
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new List<Product>();
            }

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            IProductDeserializer deserializer = new ProductDeserializer();

            return deserializer.Deserialize(content);
        }
    }
}
