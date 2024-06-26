using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFundingService
    {
        FundingModel GetFundingById(long fundingId, ref ErrorResponseModel errorResponseModel);
        List<FundingModel> GetAllFundingDetails();
        string AddFundingDetails(FundingModel funding,ref ErrorResponseModel errorResponseModel);
        string EditFunding(FundingModel funding,ref ErrorResponseModel errorResponseModel);
        string DeleteFunding(int fundingId,int userId,ErrorResponseModel errorResponseModel);

    }
}
