using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFounderVerificationService
    {
        /// <summary>
        /// Get the founder details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="founderVerifyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        FounderModelDetails GetAllFounderDetailsById(long userId,long founderVerifyId, ref ErrorResponseModel errorResponseModel);
       /// <summary>
       /// Get All founder details
       /// </summary>
       /// <returns></returns>
        List<FounderModelDetails> GetAllFounderDetails(ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All verified founder details
        /// </summary>
        /// <returns></returns>
        List<FounderModelDetails> GetAllVerifiedNonFounderDetails(bool verified, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All live preview founder details
        /// </summary>
        /// <returns></returns>
        List<FounderModelDetails> GetAllLivePreviewFounderDetails(bool live, bool preview, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All IsStealth founder details
        /// </summary>
        /// <returns></returns>
        List<FounderModelDetails> GetAllFounderIsStealthDetails(ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Send for verification founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SendForVerification(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Approve/Notpprove founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string VerificationApprove(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Make it live founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string LiveFounder(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Make it preview founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string PreviewFounder(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Founder Profile completion
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        FounderVerificationModel FounderProfileCompletion(long userId,ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Founder Status Count
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        FounderVerificationModel FounderStatusCount(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Request Raise Funding
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string RequestRaiseFunding(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get All Raise funding founder details
        /// </summary>
        /// <returns></returns>
        List<FounderModelDetails> GetAllFounderRaiseDetails(ref ErrorResponseModel errorResponseModel);
    }
}
