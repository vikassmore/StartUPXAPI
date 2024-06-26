using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IInvestmentOpportunityDetailsService
    {
        /// <summary>
        /// Add investor opportunity details
        /// </summary>
        /// <param name="investmentopportunity"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddInvestmnetOpportunity(InvestmnetopportunityModel investmentopportunity, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get the founder details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        InvestmnetopportunityModel GetInvestmentOpportunityById(long userId,  ref ErrorResponseModel errorResponseModel);
    }
}
