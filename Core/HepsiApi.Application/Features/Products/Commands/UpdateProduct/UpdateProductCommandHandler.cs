using HepsiApi.Application.Bases;
using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : BaseHandler , IRequestHandler<UpdateProductCommandRequest,Unit>
    {

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }


        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (product is null)
            {
                throw new KeyNotFoundException("Product not found or already deleted.");

            }

            var map = _mapper.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>()
                                          .GetAllAsync(x => x.ProductId == request.Id);

            await _unitOfWork.GetWriteRepository<ProductCategory>()
                  .HardDeleteRangeAsync(productCategories);

            foreach(var categoryId in request.CategoryIds)
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>()
                      .AddAsync(new ProductCategory { ProductId = request.Id, CategoryId = categoryId });
            }

            await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await _unitOfWork.SaveAsync();

            return Unit.Value;

        }
    }
}
