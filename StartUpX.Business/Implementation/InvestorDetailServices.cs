using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class InvestorDetailServices : IInvestorDetailService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public InvestorDetailServices(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="investor"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string AddInvestor(InvestorDetailModel investor,  ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
          
            var existingRecord = _startupContext.InvestorDetails.Any(x => x.EmailId == investor.EmailId && x.UserId == investor.LoggedUserId && x.IsActive == true);
            if (!existingRecord)
            {
                var investorEntity = new InvestorDetail();
                investorEntity.ProfileType = investor.ProfileType;
                investorEntity.FirstName = investor.FirstName;
                investorEntity.LastName = investor.LastName;
                investorEntity.EmailId = investor.EmailId;
                investorEntity.MobileNo = investor.MobileNo;   
                investorEntity.FounderTypeId = investor.FounderTypeId;
                investorEntity.CountryId = investor.CountryId;
                investorEntity.StateId = investor.StateId;
                investorEntity.CityId = investor.CityId;
                investorEntity.ZipCode = investor.ZipCode;
                investorEntity.Address1 = investor.Address1;
                investorEntity.Address2 = investor.Address2;
                investorEntity.CreatedDate = DateTime.Now;
                investorEntity.CreatedBy = investor.LoggedUserId;
                investorEntity.UserId = investor.LoggedUserId;
                investorEntity.IsActive = true;
                _startupContext.InvestorDetails.Add(investorEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Investor";
                userAuditLog.Description = "Investor Details Added";
                userAuditLog.UserId = investor.LoggedUserId;
                userAuditLog.CreatedBy = investor.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var investorEntity = _startupContext.InvestorDetails.Where(x => x.InvestorId == investor.InvestorId && x.UserId == investor.LoggedUserId && x.IsActive == true).FirstOrDefault();
                if (investorEntity != null)
                {
                    investorEntity.InvestorId = investor.InvestorId;
                    investorEntity.ProfileType= investor.ProfileType;
                    investorEntity.FirstName = investor.FirstName;
                    investorEntity.LastName = investor.LastName;
                    investorEntity.EmailId = investor.EmailId;
                    investorEntity.MobileNo = investor.MobileNo;
                    investorEntity.FounderTypeId = investor.FounderTypeId;
                    investorEntity.CountryId = investor.CountryId;
                    investorEntity.StateId = investor.StateId;
                    investorEntity.CityId = investor.CityId;
                    investorEntity.ZipCode = investor.ZipCode;
                    investorEntity.Address1 = investor.Address1;
                    investorEntity.Address2 = investor.Address2;
                    investorEntity.UpdatedDate = DateTime.Now;
                    investorEntity.UpdatedBy = investor.LoggedUserId;
                    investorEntity.IsActive = true;
                    _startupContext.InvestorDetails.Update(investorEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Investor";
                    userAuditLog.Description = "Investor Details Updated";
                    userAuditLog.UserId = investor.LoggedUserId;
                    userAuditLog.CreatedBy = investor.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }


            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="investorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string DeleteInvestor(int investorId, ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="investor"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public string EditInvestor(InvestorDetailModel investor, ref ErrorResponseModel errorResponseModel)
        //{
        //    var message = string.Empty;

        //    var investorEntity = _startupContext.InvestorDetails.Where(x => x.InvestorId == investor.InvestorId && x.IsActive == true).FirstOrDefault();
        //    if (investorEntity != null)
        //    {
        //        investorEntity.InvestorId = investor.InvestorId;
        //        //investorEntity.ProfileType= investor.ProfileType;
        //        investorEntity.FirstName = investor.FirstName;
        //        investorEntity.LastName = investor.LastName;
        //        investorEntity.EmailId = investor.EmailId;
        //        investorEntity.Logo = investor.Logo;
        //        investorEntity.FounderTypeId = investor.FounderTypeId;
        //        investorEntity.CountryId = investor.CountryId;
        //        investorEntity.StateId = investor.StateId;
        //        investorEntity.CityId = investor.CityId;
        //        investorEntity.ZipCode = investor.ZipCode;
        //        investorEntity.Address1 = investor.Address1;
        //        investorEntity.Address2 = investor.Address2;
        //        investorEntity.UpadteDate = DateTime.Now;
        //        investorEntity.UpadateBy = investor.LoggedUserId;
        //        _startupContext.InvestorDetails.Update(investorEntity);
        //        _startupContext.SaveChanges();
        //        message = GlobalConstants.RecordUpdateMessage;
        //    }
        //    else
        //    {
        //        message = GlobalConstants.Status500Message;
        //    }
        //    return message;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<InvestorDetailModelList> GetAllInvestor()
        {
            var investorEntity = _startupContext.InvestorDetails.Where(x => x.IsActive == true).ToList();
            var investorList = investorEntity.Select(x => new InvestorDetailModelList
            {
                InvestorId = x.InvestorId,
                ProfileType = x.ProfileType,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailId = x.EmailId,
                MobileNo = x.MobileNo,
                FounderTypeId = x.FounderTypeId,
                CountryId = x.CountryId,
                StateId = x.StateId,
                CityId = x.CityId,
                ZipCode = x.ZipCode,
                Address1 = x.Address1,
                Address2 = x.Address2
            }).ToList();
            return investorList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="investorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public InvestorDetailModelList GetInvestorById(long UserId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var investorList = new InvestorDetailModelList();
            var investorEntity = _startupContext.InvestorDetails.Include(x => x.FounderType).Include(x=>x.Country).Include(x=>x.State).Include(x=>x.City).FirstOrDefault(x => x.UserId == UserId && x.IsActive == true);
            if (investorEntity != null)
            {
                investorList.InvestorId = investorEntity.InvestorId;
                investorList.ProfileType = investorEntity.ProfileType;
                investorList.FirstName = investorEntity.FirstName;
                investorList.LastName = investorEntity.LastName;
                investorList.EmailId = investorEntity.EmailId;
                investorList.MobileNo = investorEntity.MobileNo;
                investorList.FounderTypeId = investorEntity.FounderTypeId;
                investorList.FounderTypeName = investorEntity.FounderType.FounderName;
                investorList.CountryId = investorEntity.CountryId;
                investorList.CountryName = investorEntity.Country.CountryName;
                investorList.StateId = investorEntity.StateId;
                investorList.StateName = investorEntity.State.StateName;
                investorList.CityId = investorEntity.CityId;
                investorList.CityName = investorEntity.City.CityName;
                investorList.ZipCode = investorEntity.ZipCode;
                investorList.Address1 = investorEntity.Address1;
                investorList.Address2 = investorEntity.Address2;
            }
            else
            {
                var userEntity = _startupContext.UserMasters.Include(x => x.FounderType).FirstOrDefault(x => x.UserId == UserId && x.IsActive == true);
                if (userEntity != null)
                {


                    investorList.FirstName = userEntity.FirstName;
                    investorList.LastName = userEntity.LastName;
                    investorList.EmailId = userEntity.EmailId;
                    investorList.FounderTypeId = userEntity.FounderTypeId;
                    investorList.FounderTypeName = userEntity.FounderType.FounderName;
                }
            }
            return investorList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public InvestorDetailModelList GetInvestorByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var investorEntity = _startupContext.InvestorDetails.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
            var investorDetailModel = new InvestorDetailModelList();
            if (investorEntity != null)
            {
                investorDetailModel.InvestorId = investorEntity.InvestorId;
                investorDetailModel.ProfileType = investorEntity.ProfileType;
                investorDetailModel.FirstName = investorEntity.FirstName;
                investorDetailModel.LastName = investorEntity.LastName;
                investorDetailModel.EmailId = investorEntity.EmailId;
                investorDetailModel.MobileNo = investorEntity.MobileNo;
                investorDetailModel.FounderTypeId = investorEntity.FounderTypeId;
                investorDetailModel.CountryId = investorEntity.CountryId;
                investorDetailModel.StateId = investorEntity.StateId;
                investorDetailModel.CityId = investorEntity.CityId;
                investorDetailModel.ZipCode = investorEntity.ZipCode;
                investorDetailModel.Address1 = investorEntity.Address1;
                investorDetailModel.Address2 = investorEntity.Address2;
            }
            return investorDetailModel;
        }
    }
}
