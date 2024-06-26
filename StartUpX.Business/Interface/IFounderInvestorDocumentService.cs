using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFounderInvestorDocumentService
    {
        /// <summary>
        /// Add Document details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddDocument(FounderInvestorDocumentModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All document by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<FounderInvestorDocumentModel> GetAllDocumentByUserId(long userId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// delete document
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDocument(int documentId,int userId, ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get document by user Id & documentId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        FounderInvestorDocumentModel GetDocumentByUserId(long userId,long documentId);

        /// <summary>
        /// Check document with same name exist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentName"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        bool CheckDocumentByUserId(long userId, string documentName);
    }
}
