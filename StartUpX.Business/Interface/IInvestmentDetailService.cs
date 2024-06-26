using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IInvestmentDetailService
    {
        InvestmentDetailModel GetInvestmentDetailById(long investmentId, ref ErrorResponseModel errorResponseModel);
        List<InvestmentDetailModel> GetAllInvestmentDetail();
        string AddInvestmentDetail(InvestmentDetailModel investmentDetail, ref ErrorResponseModel errorResponseModel);
        string EditInvestmentDetail(InvestmentDetailModel investmentDetail, ref ErrorResponseModel errorResponseModel);
        string DeleteInvestmentDetail(int investmentId, ErrorResponseModel errorResponseModel);

        InvestmentDetailModel GetInvestmentDetailByuserId(long userId, ref ErrorResponseModel errorResponseModel);
    }
}
