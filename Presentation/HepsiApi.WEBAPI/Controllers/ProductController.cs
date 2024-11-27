using HepsiApi.Application.Features.Products.Commands.CreateProduct;
using HepsiApi.Application.Features.Products.Commands.DeleteProduct;
using HepsiApi.Application.Features.Products.Commands.UpdateProduct;
using HepsiApi.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HepsiApi.WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest commandRequest)
        {
            await _mediator.Send(commandRequest);
            return Ok();
            
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest commandRequest)
        {
            await _mediator.Send(commandRequest);
            return Ok();
            
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest commandRequest)
        {
            await _mediator.Send(commandRequest);
            return Ok();
            
        }

    }
}
