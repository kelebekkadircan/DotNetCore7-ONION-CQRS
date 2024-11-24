using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = new(
                request.Title,
                request.Description,
                request.BrandId,
                request.Price,
                request.Discount
                    );

            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);
            
            if(await _unitOfWork.SaveAsync() > 0 )
            {
                foreach(var categoryId  in request.CategoryIds)
                {
                    ProductCategory productCategory = new()
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    };
                    await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(productCategory);

                }
                await _unitOfWork.SaveAsync();
            }

            




        }
    }
}
