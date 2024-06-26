using Microsoft.Extensions.Configuration;
using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class StateService : IStateService
    {
        StartUpDBContext _startupContext;

        public StateService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;

        }
        /// <summary>
        /// Get State By Country
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<StateModel> GetStateById(long countryId, ref ErrorResponseModel errorResponseModel)
        {
           errorResponseModel = new ErrorResponseModel();
            var stateList = new List<StateModel>();
            var stateEntity = _startupContext.States.Where(x => x.CountryId == countryId && x.IsActive == true).ToList();
            foreach (var item in stateEntity)
            {
                var model = new StateModel();
                model.StateId = item.StateId;
                model.StateName = item.StateName;
                model.StateCode= item.StateCode;
                model.CountryId = item.CountryId;
                model.IsActive = item.IsActive;

                stateList.Add(model);
            }
           return stateList;
        }
        
    }
}

