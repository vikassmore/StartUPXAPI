using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAccreditedInvestor
    {
        AccreditedInvestorModel GetAccreditedInvestorById(long accreditedInvestorId, ref ErrorResponseModel errorResponseModel);
        List<AccreditedInvestorModel> GetAllAccreditdInvestor();
        string AddAccreditedInvestor(AccreditedInvestorModel investor, ref ErrorResponseModel errorResponseModel);
        string EditAccreditedInvestor(AccreditedInvestorModel investor, ref ErrorResponseModel errorResponseModel);
        string DeleteAccreditedInvestor(int accreditedInvestorId, ErrorResponseModel errorResponseModel);
    }
}
