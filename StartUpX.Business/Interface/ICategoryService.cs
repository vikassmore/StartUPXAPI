using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICategoryService
    {
        CategoryModel GetCategoryById(long categoryId, ref ErrorResponseModel errorResponseModel);
        List<CategoryModel> GetAllCategory();
        string AddCategory(CategoryModel category, ref ErrorResponseModel errorResponseModel);
        string EditCategory(CategoryModel category, ref ErrorResponseModel errorResponseModel);
        string DeleteCategory(int categoryId, ErrorResponseModel errorResponseModel);
    }
}
