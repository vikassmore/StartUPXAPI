using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class FounderTypeService : IFounderTypeServicecs
    {
        StartUpDBContext _startupContext;

        public FounderTypeService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;

        }
        /// <summary>
        /// Get All Founder Type
        /// </summary>
        /// <returns></returns>
        public List<FounderTypeModel> GetAllfounderType()
        {
            var founderTypeEntity = _startupContext.FounderTypes.Where(x => x.IsActive == true).ToList();
            var foundeTyperList = founderTypeEntity.Select(x => new FounderTypeModel
            {
                FounderTypeId = x.FounderTypeId,
                FounderName = x.FounderName,
                IsActive = x.IsActive,


            }).ToList();
            return foundeTyperList;
        }

        public FounderTypeModel GetFounderTypeByUserId(int userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userExist = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            if (userExist == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
                return null;
            }

            var userEntity = (from user in _startupContext.UserMasters

                              join foundertype in _startupContext.FounderTypes
                              on user.FounderTypeId equals foundertype.FounderTypeId

                              where user.UserId == userId
                              select new FounderTypeModel 
                              {
                              FounderTypeId=  user.FounderTypeId,
                               FounderName= foundertype.FounderName



                              }).FirstOrDefault();
            return userEntity;
        }
    }

        
    }

