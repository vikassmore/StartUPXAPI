using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFundingDetailsServices
    {
        FundingDetailsModel GetFundingDetailsById(long fundingDetailsId, ref ErrorResponseModel errorResponseModel);
        List<FundingDetailsModel> GetAllFundingDetails();
        string AddFundingDetails(FundingDetailsModel fundingDetails, ref ErrorResponseModel errorResponseModel);
        string EditFundingDetails(FundingDetailsModel fundingDetails, ref ErrorResponseModel errorResponseModel);
        string DeleteFundingDetails(int fundingDetailsId, ErrorResponseModel errorResponseModel);

        List<FundingDetailsModel> GetAllFundingbyuserId(int userId);
    }
}
