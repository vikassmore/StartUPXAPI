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

    public class InvestmentOpportunityDetailsService : IInvestmentOpportunityDetailsService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;

        public InvestmentOpportunityDetailsService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        public string AddInvestmnetOpportunity(InvestmnetopportunityModel investmnetopportunity, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.InvestmentOpportunityDetails.Any(x => x.InvestmentOpportunityId == investmnetopportunity.InvestmentOpportunityId && x.IsActive == true);
            if (!existingRecord)
            {
                var OpportunityEntity = new InvestmentOpportunityDetail();
                var startupDetails = _startupContext.StartUpDetails.Where(x => x.UserId == investmnetopportunity.UserId).FirstOrDefault();
                var fundingDetails = _startupContext.FundingDetails.Where(x => x.UserId == investmnetopportunity.UserId).FirstOrDefault();
                var LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == investmnetopportunity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                // var SeecondLastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == investmnetopportunity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Skip(1).Select(x => x.IssuePrice).FirstOrDefault();
                double SharesOutstandingSum = _startupContext.FundingDetails.Where(x => x.UserId == investmnetopportunity.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                var LastValuation = Convert.ToDouble(LastRoundPrice) * SharesOutstandingSum;
                OpportunityEntity.SalesFee = investmnetopportunity.SalesFee;
                OpportunityEntity.ExpectedSharePrice = investmnetopportunity.ExpectedSharePrice;
                OpportunityEntity.MinimumInvestmentSize = investmnetopportunity.MinimumInvestmentSize;
                OpportunityEntity.ImpliedCompanyValuation = LastValuation.ToString();
                OpportunityEntity.LatestPostMoneyValuation = LastRoundPrice;
                OpportunityEntity.Discount = investmnetopportunity.Discount;
                OpportunityEntity.FundName = startupDetails.StartUpName;
                OpportunityEntity.FundStrategy = fundingDetails.ShareClass;
                OpportunityEntity.SecurityType = fundingDetails.ShareClass;
                OpportunityEntity.UserId = investmnetopportunity.UserId;
                OpportunityEntity.CreatedDate = DateTime.Now;
                OpportunityEntity.CreatedBy = investmnetopportunity.LoggedUserId;
                OpportunityEntity.IsActive = true;
                _startupContext.InvestmentOpportunityDetails.Add(OpportunityEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.InvestmentOpportunityMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Investment Opportunity";
                userAuditLog.Description = "Investment Opportunity Details Added";
                userAuditLog.UserId = investmnetopportunity.UserId;
                userAuditLog.CreatedBy = investmnetopportunity.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;
        }

        /// <summary>
        /// Get the founder details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestmnetopportunityModel GetInvestmentOpportunityById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var invOpportunityModel = new InvestmnetopportunityModel();
            var investmentOpportunityEntity = _startupContext.InvestmentOpportunityDetails.Where(x => x.UserId == userId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
            if (investmentOpportunityEntity != null)
            {
                invOpportunityModel.InvestmentOpportunityId = investmentOpportunityEntity.InvestmentOpportunityId;
                invOpportunityModel.FundName = investmentOpportunityEntity.FundName;
                invOpportunityModel.FundStrategy = investmentOpportunityEntity.FundStrategy;
                invOpportunityModel.SalesFee = investmentOpportunityEntity.SalesFee;
                invOpportunityModel.ExpectedSharePrice = investmentOpportunityEntity.ExpectedSharePrice;
                invOpportunityModel.SecurityType = investmentOpportunityEntity.SecurityType;
                invOpportunityModel.ImpliedCompanyValuation = investmentOpportunityEntity.ImpliedCompanyValuation;
                invOpportunityModel.LatestPostMoneyValuation = investmentOpportunityEntity.LatestPostMoneyValuation;
                invOpportunityModel.Discount = investmentOpportunityEntity.Discount;
                invOpportunityModel.MinimumInvestmentSize = investmentOpportunityEntity.MinimumInvestmentSize;
            }
            return invOpportunityModel;
        }
    }
}
