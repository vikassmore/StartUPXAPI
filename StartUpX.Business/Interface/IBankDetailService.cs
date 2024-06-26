using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IBankDetailService
    {
        BankAccountDetailModel GetBankDetailById(long bankId, ref ErrorResponseModel errorResponseModel);
        List<BankAccountDetailModel> GetAllBankDetail();
        string AddBankDetail(BankAccountDetailModel bankdetail, ref ErrorResponseModel errorResponseModel);
        string DeleteBankDetail(int bankId, ErrorResponseModel errorResponseModel);

        BankAccountDetailModel GetBankDetailByuserId(long userId, ref ErrorResponseModel errorResponseModel);

    }
}
