using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(p => true).Any();
            if (!existProduct)
            {
                products.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "60fc468ffc13ae29fa000000",
                    Name = "Borracha",
                    Category = "Escolar",
                    Description = "Material escolar",
                    Image = "https://robohash.org/doloresametminima.png?size=50x50&set=set1",
                    Price = 2.50m
                },
                new Product
                {
                    Id = "60fc468ffc13ae29fa000001",
                    Name = "Caderno",
                    Category = "Escolar",
                    Description = "Material escolar",
                    Image = "https://robohash.org/eaqueitaquecorporis.png?size=50x50&set=set1",
                    Price = 15.00m
                },
                new Product
                {
                    Id = "60fc468ffc13ae29fa000017",
                    Name = "Caneta",
                    Category = "Escolar",
                    Description = "Material escolar",
                    Image = "https://robohash.org/natuseaaut.png?size=50x50&set=set1",
                    Price = 3.00m
                }
            };
        }
    }
}
