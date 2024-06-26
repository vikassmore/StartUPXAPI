using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IStartupDetailsService
    {
        StartupDeatailModelList GetStartupById(long startupId, ref ErrorResponseModel errorResponseModel);
        List<StartupDeatailModelList> GetAllStartup();
        string AddStartup(StartupDeatailModel startup, byte[] logo,string fileName, ref ErrorResponseModel errorResponseModel);
        string DeleteStartup(int startupId, ErrorResponseModel errorResponseModel);

        StartupDeatailModelList GetStartupByuserId(long userId, ref ErrorResponseModel errorResponseModel);
    }
}
