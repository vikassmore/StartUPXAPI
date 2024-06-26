using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFounderDetailService
    {
        FounderDeatailModel GetFounderById(long founderId, ref ErrorResponseModel errorResponseModel);
        List<FounderDeatailModel> GetAllFounder();
        string AddFounder(List<FounderDeatailModel> model,int LoggedUserId, ref ErrorResponseModel errorResponseModel);
        string DeleteFounder(int founderId,int LoggedUserId, ErrorResponseModel errorResponseModel);

        string EditFounder(FounderDeatailModel founder, ref ErrorResponseModel errorResponseModel);

        List<FounderDeatailModel> GetAllFounderbyuserId(int userId);
    }
}
