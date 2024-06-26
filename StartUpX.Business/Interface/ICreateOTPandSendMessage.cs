using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICreateOTPandSendMessage
    {
        string CreateOTPandSendMessage(string email, ref ErrorResponseModel errorResponseModel);
        string CreateNewOTP(int userId);
    }
}
