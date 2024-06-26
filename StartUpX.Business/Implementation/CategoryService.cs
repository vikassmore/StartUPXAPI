using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class CategoryService : ICategoryService
    {

        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public CategoryService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="category"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddCategory(CategoryModel category, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existingRecord = _startupContext.CategoryMasters.Any(x => x.Name == category.Name && x.IsActive == true);
            if (!existingRecord)
            {
                var categoryEntity = new CategoryMaster();
                categoryEntity.IsActive = category.IsActive;
                categoryEntity.Name = category.Name;
                categoryEntity.Description = category.Description;
                categoryEntity.CreatedBy = category.LoggedUserId;
                categoryEntity.CreatedDate = DateTime.Now;
                _startupContext.CategoryMasters.Add(categoryEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Category Add";
                userAuditLog.Description = "Category Add.";
                userAuditLog.UserId = (int)category.LoggedUserId;
                userAuditLog.CreatedBy = category.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }

            return message;
        }
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteCategory(int categoryId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var category = new CategoryModel();
            var categoryEntity = _startupContext.CategoryMasters.Where(x => x.CategoryId == categoryId && x.IsActive == true).FirstOrDefault();
            if (categoryEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                categoryEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "category Delete";
                userAuditLog.Description = "category deleted.";
                userAuditLog.UserId = (int)category.LoggedUserId;
                userAuditLog.CreatedBy = category.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }

            return message;
        }
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="category"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditCategory(CategoryModel category, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var categoryEntity = _startupContext.CategoryMasters.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
            if (categoryEntity != null)
            {
                categoryEntity.CategoryId = category.CategoryId;
                categoryEntity.Name = category.Name;
                categoryEntity.Description = category.Description;
                categoryEntity.IsActive = true;
                categoryEntity.UpdatedBy = category.LoggedUserId;
                categoryEntity.UpdatedDate = DateTime.Now;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "category Edit";
                userAuditLog.Description = "category updated.";
                userAuditLog.UserId = (int)category.LoggedUserId;
                userAuditLog.CreatedBy = category.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }

            return message;
        }
        /// <summary>
        /// Get All Category
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> GetAllCategory()
        {
            var categoryEntity = _startupContext.CategoryMasters.Where(x => x.IsActive == true).ToList();
            var categoryList = categoryEntity.Select(x => new CategoryModel
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,

            }).ToList();
            return categoryList;
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public CategoryModel GetCategoryById(long categoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var categoryModel = new CategoryModel();
            var categoryEntity = _startupContext.CategoryMasters.FirstOrDefault(x => x.CategoryId == categoryId && x.IsActive == true);
            if (categoryEntity != null)
            {
                categoryModel.CategoryId = categoryEntity.CategoryId;
                categoryModel.Name = categoryEntity.Name;
                categoryModel.Description = categoryEntity.Description;
                categoryModel.IsActive = categoryEntity.IsActive;
            }
            return categoryModel;
        }
    }
}
