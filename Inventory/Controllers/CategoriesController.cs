using Business_Layer.Services;
using DAL.Helpers.Common;
using DAL.Persistence.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;   
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto category)
        {
            try
            {
                await _categoriesService.CreateCategory(category);

                return Ok(new BaseResponse { StatusCode = "200:Category Created successfuly" });
            }
            catch (Exception e) { return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } }); }

        }

        [HttpPost("GetAllCategories")]
        public async Task<ActionResult> GetAllCategories(PaginationParameters paginationParameters)
        {
            try
            {
                var result = await _categoriesService.GetAllCategories(paginationParameters);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });

            }
        }

        [HttpPost("UpdateCategory")]
        public async Task<ActionResult> UpdateCategory(CreateCategoryDto category)
        {
            try
            {
               var result = await _categoriesService.UpdateCategory(category);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });
            }
        }

        [HttpPost("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(Guid Id)
        {
            try
            {
                await _categoriesService.DeleteCategory(Id);
                return Ok(new BaseResponse { StatusCode = "200:Category Deleted successfuly" });
            }
            catch (Exception e)
            {
                return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });
            }
        }

        //[HttpGet("GetFirstForTest")]
        //public async Task<ActionResult> GetFirstForTest()
        //{
        //    try
        //    {
        //        await _categoriesService.GetAllCategories(Id);
        //        return Ok(new BaseResponse { StatusCode = "200:Category Deleted successfuly" });
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = e.ToString() } } });
        //    }
        //}



    }
}
