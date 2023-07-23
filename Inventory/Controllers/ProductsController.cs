using Business_Layer.Services;
using DAL.Helpers.Common;
using DAL.Persistence.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsServices _productsServices;
        public ProductsController(IProductsServices productsServices)
        {
            _productsServices = productsServices;  
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto product)
        {
            try
            {
                await _productsServices.CreateProduct(product);

                return Ok(new BaseResponse { StatusCode = "200:Product Created successfuly" });
            }
            catch (Exception e) { return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } }); }

        }

        [HttpPost("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts(PaginationParameters paginationParameters)
        {
            try
            {
                var result = await _productsServices.GetAllProducts(paginationParameters);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });

            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(CreateProductDto product)
        {
            try
            {
                var result = await _productsServices.UpdateProduct(product);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });
            }
        }

        [HttpPost("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(Guid Id)
        {
            try
            {
                await _productsServices.DeleteProduct(Id);
                return Ok(new BaseResponse { StatusCode = "200:Category Deleted successfuly" });
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });
            }
        }
    }
}
