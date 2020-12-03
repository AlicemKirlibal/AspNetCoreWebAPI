using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;

namespace TestProject.Data.Seeds
{
    class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids;

        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }



        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Product1", Price = 82.30m, Stock = 20, CategoryId = _ids[0] },
               new Product { Id = 2, Name = "Product2", Price = 123.30m, Stock = 20, CategoryId = _ids[0] },
                new Product { Id = 3, Name = "Product3", Price = 42.30m, Stock = 20, CategoryId = _ids[1] },
                new Product { Id = 4, Name = "Product4", Price = 62.30m, Stock = 20, CategoryId = _ids[1] },
                new Product { Id = 5, Name = "Product5", Price = 17.30m, Stock = 20, CategoryId = _ids[1] }
                );

        }
    }
}
