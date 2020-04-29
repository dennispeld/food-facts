using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IProductDeserializer
    {
        List<Product> Deserialize(string rawProducts);
    }
}
