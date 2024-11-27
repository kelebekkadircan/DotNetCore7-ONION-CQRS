using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Products.Rules;
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

namespace HepsiApi.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler :BaseHandler ,  IRequestHandler<CreateProductCommandRequest , Unit>
    {
        private readonly ProductRules _productRules;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ProductRules productRules, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _productRules = productRules;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
           IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

            

            await _productRules.ProductTitleMustNotBeSame(products , request.Title);

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

            return Unit.Value;





        }
    }
}
