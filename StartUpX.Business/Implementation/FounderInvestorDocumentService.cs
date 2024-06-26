using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class FounderInvestorDocumentService : IFounderInvestorDocumentService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public FounderInvestorDocumentService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        /// <summary>
        /// Add Document details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddDocument(FounderInvestorDocumentModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var existingRecord = _startupContext.FounderInvestorDocuments.Any(x => x.DocumentName == model.DocumentName && x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                var documentEntity = new FounderInvestorDocument();
                documentEntity.IsActive = true;
                documentEntity.UserId = model.UserId;
                documentEntity.DocumentName = model.DocumentName;
                documentEntity.FileName = model.FileName;
                documentEntity.FilePath = model.FilePath;
                documentEntity.CreatedBy = model.LoggedUserId;
                documentEntity.CreatedDate = DateTime.Now;
                _startupContext.FounderInvestorDocuments.Add(documentEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.DocumentUploadedMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Document Add";
                userAuditLog.Description = "Document Added";
                userAuditLog.UserId = model.UserId;
                userAuditLog.CreatedBy = model.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.SameDocumentUploadedMessage;
            }
            return message;
        }
        /// <summary>
        /// Get All document by Id
        /// </summary>
        /// <returns></returns>
        public List<FounderInvestorDocumentModel> GetAllDocumentByUserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var documentEntity = _startupContext.FounderInvestorDocuments.Where(x =>x.UserId == userId && x.IsActive == true).ToList();
            var documentList = documentEntity.Select(x => new FounderInvestorDocumentModel
            {
                DocumentId = x.DocumentId,
                DocumentName = x.DocumentName,
                FileName = x.FileName,
                FilePath = x.FilePath,
                UserId = x.UserId

            }).ToList();
            return documentList;
        }
        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteDocument(int documentId,int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var documentEntity = _startupContext.FounderInvestorDocuments.Where(x => x.DocumentId == documentId && x.IsActive == true).FirstOrDefault();
            if (documentEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                documentEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.DocumentDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Document";
                userAuditLog.Description = "Document Deleted.";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
        /// <summary>
        /// Get document by user Id & documentId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FounderInvestorDocumentModel GetDocumentByUserId(long userId,long documentId)
        {
            var model = new FounderInvestorDocumentModel();
            var documentEntity = _startupContext.FounderInvestorDocuments.FirstOrDefault(x => x.UserId == userId &&x.DocumentId == documentId && x.IsActive == true);
           if(documentEntity != null)
            {              
                model.DocumentId = documentEntity.DocumentId;
                model.DocumentName = documentEntity.DocumentName;
                model.FileName = documentEntity.FileName;
                model.FilePath = documentEntity.FilePath;
                model.UserId = documentEntity.UserId;
            }
            else
            {
                return null;
            }
            return model;
        }
        /// <summary>
        /// Check document with same name exist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentName"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public bool CheckDocumentByUserId(long userId, string documentName)
        {
            bool documentEntity = _startupContext.FounderInvestorDocuments.Any(x => x.UserId == userId && x.DocumentName == documentName && x.IsActive == true);
            if (!documentEntity)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
