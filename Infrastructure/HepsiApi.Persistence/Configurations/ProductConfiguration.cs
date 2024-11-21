using Bogus;
using HepsiApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            Faker faker = new("tr");

            Product product1 = new()
            {
                Id = 1,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription() ,
                BrandId = 1,
                Price = faker.Random.Decimal(1, 1000),
                Discount = faker.Random.Int(1, 100),
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            Product product2 = new()
            {
                Id = 2,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 2,
                Price = faker.Random.Decimal(1, 1000),
                Discount = faker.Random.Int(1, 100),
                CreatedDate = DateTime.Now,
                IsDeleted = true
            };
            Product product3 = new()
            {
                Id = 3,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 3,
                Price = faker.Random.Decimal(1, 1000),
                Discount = faker.Random.Int(1, 100),
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            builder.HasData(product1, product2, product3);
        }
    }
}
