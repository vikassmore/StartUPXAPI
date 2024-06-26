using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface INotableInvestorService
    {
        NotableInvestorModel GetNotableInvestorById(int notableInvestorId, ref ErrorResponseModel errorResponseModel);
       
        List<NotableInvestorModel> GetAllNotableInvestor();
       
        string AddNotableInvestor(NotableInvestorModel notableinvestor, ref ErrorResponseModel errorResponseModel);
        
        string EditNotableInvestor(NotableInvestorModel notableinvestor, ref ErrorResponseModel errorResponseModel);
       
        string DeleteNotableInvestor(int notableInvestorId,int userId, ErrorResponseModel errorResponseModel);
    }
}
