using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    /// <summary>
    /// Register the dependencies
    /// </summary>
    public static class ServiceRegistrar
    {
        /// <summary>
        /// An extension to IServiceCollection that will add my dependencies into services.
        /// </summary>
        /// <param name="services"></param>
        public static void LoadServices(this IServiceCollection services)
        {
            services.AddHttpClient<IFoodFactsService, FoodFactsService>();
        }
    }
}
