using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IInvestorDetailService
    {

        InvestorDetailModelList GetInvestorById(long UserId, ref ErrorResponseModel errorResponseModel);
            List<InvestorDetailModelList> GetAllInvestor();
            string AddInvestor(InvestorDetailModel investor, ref ErrorResponseModel errorResponseModel);
            //string EditInvestor(InvestorDetailModel investor, ref ErrorResponseModel errorResponseModel);
            string DeleteInvestor(int investorId, ErrorResponseModel errorResponseModel);

        InvestorDetailModelList GetInvestorByuserId(long userId, ref ErrorResponseModel errorResponseModel);
        
    }
}
