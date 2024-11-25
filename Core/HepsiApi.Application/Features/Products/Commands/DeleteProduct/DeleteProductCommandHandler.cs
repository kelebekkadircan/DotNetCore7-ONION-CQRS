using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest , Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            //var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            //product.IsDeleted = true;

            //await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
            //await _unitOfWork.SaveAsync();
            // Ürünü Getir ve Durumunu Güncelle
            var productRepository = _unitOfWork.GetReadRepository<Product>();
            var product = await productRepository.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (product is null)
            {
                // Hata Fırlat ya da Ürünün zaten silindiğini belirt.
                throw new KeyNotFoundException("Product not found or already deleted.");
            }

            // Soft Delete İşlemi
            product.IsDeleted = true;

            // Güncellenen Ürünü Kaydet
            await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);

            // Değişiklikleri Veritabanına Yansıt
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
