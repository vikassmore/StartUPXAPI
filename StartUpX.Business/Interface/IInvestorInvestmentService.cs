using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IInvestorInvestmentService
    {
        /// <summary>
        /// Get the Invested details by User ID
        /// </summary>
        /// <param name="investorUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<InvestorInvestmentList> GetAllInvestmentById(long investorUserId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get the Invested details set as WatchList
        /// </summary>
        /// <param name="onWatch"></param>
        /// <param name="investorUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<InvestorInvestmentList> GetAllInvestmentOnWatch(bool onWatch, long investorUserId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Add Indicate Interest of Investor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddIndicateInterest(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Add Investor Investment
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddInvestorInvestment(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Add Comapny as On Watch
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddOnWatch(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Add to Request Offering
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddRequestOffering(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// delete from watch list
        /// </summary>
        /// <param name="investorInvestmentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteFromWatchlist(int investorInvestmentId,int userId, ErrorResponseModel errorResponseModel);
    }
}
