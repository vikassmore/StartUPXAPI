using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class CityService : ICityService
    {
        StartUpDBContext _startupContext;

        public CityService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;

        }
        /// <summary>
        /// Get All City By State
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<CityModel> GetCityById(long stateId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cityList = new List<CityModel>();
            var cityEntity = _startupContext.Cities.Where(x => x.StateId == stateId && x.IsActive == true).ToList();
            foreach (var item in cityEntity)
            {
                var model = new CityModel();
                model.CityId = item.CityId;
                model.CityName = item.CityName;
                model.StateId = item.StateId;   
                model.IsActive =true;

                cityList.Add(model);
            }
            return cityList;
        }
    }
}
