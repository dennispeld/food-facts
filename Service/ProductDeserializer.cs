using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Service
{
    public class ProductDeserializer : IProductDeserializer
    {
        /// <summary>
        /// Deserialize the products
        /// </summary>
        /// <param name="rawProducts">Json string</param>
        /// <returns>List of products</returns>
        public List<Product> Deserialize(string rawProducts)
        {
            // parse raw json string into a json object
            JObject json = JObject.Parse(rawProducts);

            // get a list of products as abstract Json tokens
            List<JToken> results = json["products"].Children().ToList();

            // using LINQ retrieve a list of products in the correct format
            return results.Select(token => new Product {
                ProductName = (string)token["product_name"],
                Ingredients = ((string)token["ingredients_text"]).Split(", ").ToList<string>()
            }).ToList();
        }
    }
}
