using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using StartUpX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StartUpX.Business.Implementation
{
    public class FundingDetailsService : IFundingDetailsServices
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public FundingDetailsService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// Added Funding Detail  Data
        /// </summary>
        /// <param name="fundingDetails"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddFundingDetails(FundingDetailsModel fundingDetails, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var existingRecord = _startupContext.FundingDetails.Any(x => x.UserId == fundingDetails.LoggedUserId && x.FundingId == fundingDetails.FundingId && x.IsActive == true);
            if (!existingRecord)
            {
                var fundingDetailsEntity = new FundingDetail();

                //fundingDetailsEntity.SeriesName=fundingDetails.SeriesName;
                fundingDetailsEntity.FundingId = fundingDetails.FundingId;
                fundingDetailsEntity.ShareClass = fundingDetails.ShareClass;
                fundingDetailsEntity.DateFinancing = fundingDetails.DateFinancing;
                fundingDetailsEntity.SharesOutstanding = fundingDetails.SharesOutstanding;
                fundingDetailsEntity.IssuePrice = fundingDetails.IssuePrice;
                fundingDetailsEntity.ConversionPrice = fundingDetails.ConversionPrice;
                fundingDetailsEntity.TotalFinancingSize = fundingDetails.TotalFinancingSize;
                fundingDetailsEntity.LiquidationPreference = fundingDetails.LiquidationPreference;
                fundingDetailsEntity.LiquidityRank = fundingDetails.LiquidityRank;
                fundingDetailsEntity.DividendRate = fundingDetails.DividendRate;
                fundingDetailsEntity.DividendType = fundingDetails.DividendType;
                fundingDetailsEntity.VotesPerShare = fundingDetails.VotesPerShare;
                fundingDetailsEntity.RedemptionRights = fundingDetails.RedemptionRights;
                fundingDetailsEntity.ConvertibleToOnPublicListing = fundingDetails.ConvertibleToOnPublicListing;
                fundingDetailsEntity.ParticipatingPreferred = fundingDetails.ParticipatingPreferred;
                fundingDetailsEntity.QualifiedIpo = fundingDetails.QualifiedIpo;
                fundingDetailsEntity.OtherKeyProvisions = fundingDetails.OtherKeyProvisions;
                fundingDetailsEntity.CreatedDate = DateTime.Now;
                fundingDetailsEntity.CreatedBy = fundingDetails.LoggedUserId;
                fundingDetailsEntity.UserId = fundingDetails.LoggedUserId;
                fundingDetailsEntity.IsActive = true;
                _startupContext.FundingDetails.Add(fundingDetailsEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Funding Details";
                userAuditLog.Description = "Fundining Details Added";
                userAuditLog.UserId = fundingDetails.LoggedUserId;
                userAuditLog.CreatedBy = fundingDetails.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingSeriesMessage;
            }
            return message;
        }


        /// <summary>
        /// Deleted Funding Detail Data
        /// </summary>
        /// <param name="fundingDetailsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteFundingDetails(int fundingDetailsId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var fundingEntity = _startupContext.FundingDetails.Where(x => x.FundingDetailsId == fundingDetailsId && x.IsActive == true).FirstOrDefault();
            if (fundingEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            fundingEntity.IsActive = false;
            _startupContext.SaveChanges();
            message = GlobalConstants.RecordDeleteMessage;
            return message;
        }


        /// <summary>
        /// Edit Funding Detail Data    
        /// </summary>
        /// <param name="fundingDetails"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditFundingDetails(FundingDetailsModel fundingDetails, ref ErrorResponseModel errorResponseModel)
        {
            
            var message = string.Empty;
            var fundingDetailsEntity = _startupContext.FundingDetails.Where(x => x.FundingDetailsId == fundingDetails.FundingDetailsId && x.UserId == fundingDetails.LoggedUserId && x.FundingId == fundingDetails.FundingId && x.IsActive == true).FirstOrDefault();
            if (fundingDetailsEntity != null)
            {
                fundingDetailsEntity.FundingDetailsId = fundingDetails.FundingDetailsId;
                fundingDetailsEntity.FundingId = fundingDetails.FundingId;
                fundingDetailsEntity.ShareClass = fundingDetails.ShareClass;
                fundingDetailsEntity.DateFinancing = fundingDetails.DateFinancing;
                fundingDetailsEntity.SharesOutstanding = fundingDetails.SharesOutstanding;
                fundingDetailsEntity.IssuePrice = fundingDetails.IssuePrice;
                fundingDetailsEntity.ConversionPrice = fundingDetails.ConversionPrice;
                fundingDetailsEntity.TotalFinancingSize = fundingDetails.TotalFinancingSize;
                fundingDetailsEntity.LiquidationPreference = fundingDetails.LiquidationPreference;
                fundingDetailsEntity.LiquidityRank = fundingDetails.LiquidityRank;
                fundingDetailsEntity.DividendRate = fundingDetails.DividendRate;
                fundingDetailsEntity.DividendType = fundingDetails.DividendType;
                fundingDetailsEntity.VotesPerShare = fundingDetails.VotesPerShare;
                fundingDetailsEntity.RedemptionRights = fundingDetails.RedemptionRights;
                fundingDetailsEntity.ConvertibleToOnPublicListing = fundingDetails.ConvertibleToOnPublicListing;
                fundingDetailsEntity.ParticipatingPreferred = fundingDetails.ParticipatingPreferred;
                fundingDetailsEntity.QualifiedIpo = fundingDetails.QualifiedIpo;
                fundingDetailsEntity.OtherKeyProvisions = fundingDetails.OtherKeyProvisions;
                fundingDetailsEntity.UpdatedDate = DateTime.Now;
                fundingDetailsEntity.UpdatedBy = fundingDetails.LoggedUserId;
                fundingDetailsEntity.IsActive = true;
                _startupContext.FundingDetails.Update(fundingDetailsEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Funding Details";
                userAuditLog.Description = "Fundining Details Updated";
                userAuditLog.UserId = fundingDetails.LoggedUserId;
                userAuditLog.CreatedBy = fundingDetails.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }


        /// <summary>
        /// Getall Funding Detail Data  
        /// </summary>
        /// <returns></returns>
        public List<FundingDetailsModel> GetAllFundingDetails()
        {
            var fundingDetailsEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.IsActive == true).ToList();
            var fundingDetailsList = fundingDetailsEntity.Select(x => new FundingDetailsModel
            {
                FundingDetailsId = x.FundingDetailsId,
                FundingId = x.FundingId,
                SeriesName = x.Funding.Name,
                ShareClass = x.ShareClass,
                DateFinancing = x.DateFinancing,
                SharesOutstanding = x.SharesOutstanding,
                IssuePrice = x.IssuePrice,
                ConversionPrice = x.ConversionPrice,
                TotalFinancingSize = x.TotalFinancingSize,
                LiquidityRank = x.LiquidityRank,
                LiquidationPreference = x.LiquidationPreference,
                DividendRate = x.DividendRate,
                DividendType = x.DividendType,
                VotesPerShare = x.VotesPerShare,
                RedemptionRights = x.RedemptionRights,
                ConvertibleToOnPublicListing = x.ConvertibleToOnPublicListing,
                ParticipatingPreferred = x.ParticipatingPreferred,
                QualifiedIpo = x.QualifiedIpo,
                OtherKeyProvisions = x.OtherKeyProvisions

            }).ToList();
            return fundingDetailsList;
            //throw new NotImplementedException();
        }



        /// <summary>
        /// Getall Funding Detail Data  
        /// </summary>
        /// <returns></returns>
        public List<FundingDetailsModel> GetAllFundingbyuserId(int userId)
        {
            var fundingDetailsEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == userId && x.IsActive == true).ToList();
            var fundingDetailsList = fundingDetailsEntity.Select(x => new FundingDetailsModel
            {
                FundingDetailsId = x.FundingDetailsId,
                FundingId = x.FundingId,
                SeriesName = x.Funding.Name,
                ShareClass = x.ShareClass,
                DateFinancing = x.DateFinancing,
                SharesOutstanding = x.SharesOutstanding,
                IssuePrice = x.IssuePrice,
                ConversionPrice = x.ConversionPrice,
                TotalFinancingSize = x.TotalFinancingSize,
                LiquidityRank = x.LiquidityRank,
                LiquidationPreference = x.LiquidationPreference,
                DividendRate = x.DividendRate,
                DividendType = x.DividendType,
                VotesPerShare = x.VotesPerShare,
                RedemptionRights = x.RedemptionRights,
                ConvertibleToOnPublicListing = x.ConvertibleToOnPublicListing,
                ParticipatingPreferred = x.ParticipatingPreferred,
                QualifiedIpo = x.QualifiedIpo,
                OtherKeyProvisions = x.OtherKeyProvisions

            }).ToList();
            return fundingDetailsList;
            //throw new NotImplementedException();
        }


        /// <summary>
        /// GetFundingDetailsById Funding Detail Data
        /// </summary>
        /// <param name="fundingDetailsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FundingDetailsModel GetFundingDetailsById(long fundingDetailsId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var fundingDetailsList = new FundingDetailsModel();
            var fundingDetailsEntity = _startupContext.FundingDetails.FirstOrDefault(x => x.FundingDetailsId == fundingDetailsId && x.IsActive == true);
            if (fundingDetailsEntity != null)
            {
                fundingDetailsList.FundingDetailsId = fundingDetailsEntity.FundingDetailsId;
                //fundingDetailsEntity.SeriesName = fundingDetailsEntity.SeriesName;
                fundingDetailsList.FundingId = fundingDetailsEntity.FundingId;
                fundingDetailsList.ShareClass = fundingDetailsEntity.ShareClass;
                fundingDetailsList.DateFinancing = fundingDetailsEntity.DateFinancing;
                fundingDetailsList.SharesOutstanding = fundingDetailsEntity.SharesOutstanding;
                fundingDetailsList.IssuePrice = fundingDetailsEntity.IssuePrice;
                fundingDetailsList.ConversionPrice = fundingDetailsEntity.ConversionPrice;
                fundingDetailsList.TotalFinancingSize = fundingDetailsEntity.TotalFinancingSize;
                fundingDetailsList.LiquidityRank = fundingDetailsEntity.LiquidityRank;
                fundingDetailsList.LiquidationPreference = fundingDetailsEntity.LiquidationPreference;
                fundingDetailsList.DividendRate = fundingDetailsEntity.DividendRate;
                fundingDetailsList.DividendType = fundingDetailsEntity.DividendType;
                fundingDetailsList.VotesPerShare = fundingDetailsEntity.VotesPerShare;
                fundingDetailsList.RedemptionRights = fundingDetailsEntity.RedemptionRights;
                fundingDetailsList.ConvertibleToOnPublicListing = fundingDetailsEntity.ConvertibleToOnPublicListing;
                fundingDetailsList.ParticipatingPreferred = fundingDetailsEntity.ParticipatingPreferred;
                fundingDetailsList.QualifiedIpo = fundingDetailsEntity.QualifiedIpo;
                fundingDetailsList.OtherKeyProvisions = fundingDetailsEntity.OtherKeyProvisions;
            }
            return fundingDetailsList;

        }
    }
}
