using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IFounderTypeServicecs
    {
        List<FounderTypeModel> GetAllfounderType();
        FounderTypeModel GetFounderTypeByUserId(int userId, ref ErrorResponseModel errorResponseModel);
    }
}
