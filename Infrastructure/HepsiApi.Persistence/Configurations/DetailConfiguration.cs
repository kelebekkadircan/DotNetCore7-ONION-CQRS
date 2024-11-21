using Bogus;
using HepsiApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Persistence.Configurations
{
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
                
            Faker faker = new("tr");

            Detail detail1 = new()
            {
                Id = 1,
                Title = faker.Lorem.Sentence(1),
                Description = faker.Commerce.ProductDescription(),
                CategoryId = 1,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            Detail detail2 = new()
            {
                Id = 2,
                Title = faker.Lorem.Sentence(2),
                Description = faker.Commerce.ProductDescription(),
                CategoryId = 3,
                CreatedDate = DateTime.Now,
                IsDeleted = true
            };
            Detail detail3 = new()
            {
                Id = 3,
                Title = faker.Lorem.Sentence(1),
                Description = faker.Commerce.ProductDescription(),
                CategoryId = 4,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            builder.HasData(detail1, detail2, detail3);


        }
    }
}
