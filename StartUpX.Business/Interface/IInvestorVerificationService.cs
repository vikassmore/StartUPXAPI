using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IInvestorVerificationService
    {
        /// <summary>
        /// Get the founder details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="investorVerifyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        InvestorModel GetAllInvestorDetailsById(long userId, long investorVerifyId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        List<InvestorModel> GetAllInvestorDetails(ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All verified founder details
        /// </summary>
        /// <returns></returns>
        List<InvestorModel> GetAllVerifiedNonInvestorDetails(bool verified, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Send for verification investor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SendForVerification(InvestorVerificationModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Approve/Notpprove founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string VerificationApprove(InvestorVerificationModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Investor Profile completion
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        InvestorVerificationModel InvestorProfileCompletion(long userId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Investor Status Count
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        InvestorVerificationModel InvestorStatusCount(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get All live preview Startup details
        /// </summary>
        /// <returns></returns>
        List<FounderModelDetails> GetAllLivePreviewStartupDetails(bool live, bool preview, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get All Startup By Sector
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<FounderModelDetails> GetAllStartupDetailsBySector(long userId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        List<RequestOfferModel> GetAllrequestOfferingInvestorDetails(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        List<InvestorsInvestmentsModel> GetAllInvestmentsDetails(ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        List<IndicateInterestModel> GetAllIndicateInvestmentsDetails(ref ErrorResponseModel errorResponseModel);
    }
}
