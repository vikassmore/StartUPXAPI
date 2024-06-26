using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISocialMediaLogin
    {
        AuthModel AddSocialMediaUser(SocialMediaUserLoginModel socialmediaLogin, ref ErrorResponseModel errorResponseModel);
        SocialMediaUserLoginModel GetSocialMedialUserByEmail(string emailId, ref ErrorResponseModel errorResponseModel);
    }
}
