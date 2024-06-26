using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICityService
    {
        List<CityModel> GetCityById(long stateId, ref ErrorResponseModel errorResponseModel);

    }
}
