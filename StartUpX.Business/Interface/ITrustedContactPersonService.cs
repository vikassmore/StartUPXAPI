using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ITrustedContactPersonService
    {
        TrustedContactPersonModel GetTrustedContactById(long TrustedContactId, ref ErrorResponseModel errorResponseModel);
        List<TrustedContactPersonModel> GetAllTrustedContact();
        string AddTrustedContact(TrustedContactPersonModel trustedContact, ref ErrorResponseModel errorResponseModel);
       
        TrustedContactPersonModel GetTrustedContactByuserId(long trustedContactId, ref ErrorResponseModel errorResponseModel);
    }
}
