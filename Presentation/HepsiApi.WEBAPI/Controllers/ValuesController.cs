using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HepsiApi.WEBAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
         this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Getreadrepallasync()
        {
            var values = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
         return   Ok(values);
        }





    }
}
