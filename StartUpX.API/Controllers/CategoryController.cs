using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseAPIController
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }
        [HttpGet("getAllCategory")]

        [ProducesResponseType(typeof(CategoryModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var categoryModel = _categoryService.GetAllCategory();

                if (categoryModel != null)
                {
                    return Ok(categoryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpGet("GetcategoryById/{categoryId}")]
        //[Authorize]
        [ProducesResponseType(typeof(CategoryModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long categoryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var categoryModel = _categoryService.GetCategoryById(categoryId, ref errorResponseModel);

                if (categoryModel != null)
                {
                    return Ok(categoryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post( CategoryModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var categoryModel = _categoryService.AddCategory(model, ref errorMessage);
                if (!string.IsNullOrEmpty(categoryModel))
                {
                    return Ok(categoryModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPost("Edit")]
        public IActionResult Put(CategoryModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var categoryModel = _categoryService.EditCategory(model, ref errorMessage);
                if (!string.IsNullOrEmpty(categoryModel))
                {
                    return Ok(categoryModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete Sector Data
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int categoryId)
        {
            try
            {
                var errorMessage = new ErrorResponseModel();
                var categoryModel = _categoryService.DeleteCategory(categoryId, errorMessage);
                if (!string.IsNullOrEmpty(categoryModel))
                {
                    return Ok();
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
    }
}
