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
    public class InvestorInvestmentService : IInvestorInvestmentService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;
        public InvestorInvestmentService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// Get the Invested details by User ID
        /// </summary>
        /// <param name="investorUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<InvestorInvestmentList> GetAllInvestmentById(long investorUserId, ref ErrorResponseModel errorResponseModel)
        {
            List<InvestorInvestmentList> investmentModelList = new List<InvestorInvestmentList>();
            var investmentEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.InvestorUserId == investorUserId && x.InvestmentAmount != null && x.IsActive == true).ToList();
            foreach (var item in investmentEntity)
            {
                var investmentModel = new InvestorInvestmentList();
                investmentModel.InvestorInvestmentId = item.InvestorInvestmentId;
                investmentModel.InvestmentAmount = item.InvestmentAmount;
                investmentModel.InvestmentRound = item.InvestmentRound;
                investmentModel.IndicateInterest = item.IndicateInterest;
                investmentModel.FounderVerifyId = item.FounderVerifyId;
                investmentModel.InvestorUserId = item.InvestorUserId;
                investmentModel.IsActive = item.IsActive;
                investmentModel.OnWatchList = item.OnWatchList;
                investmentModel.CretedDate = item.CreatedDate;
                var founderEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.FounderVerifyId == item.FounderVerifyId && x.SendForVerification == true && ((x.Live == true && x.Preview == false) || (x.Live == false && x.Preview == true)) && x.Verified == true && x.IsActive == true);
                if (founderEntity != null)
                {
                    var founderModel = new FounderModelDetails();
                    founderModel.FounderVerifyId = founderEntity.FounderVerifyId;
                    founderModel.GaugingAmount = founderEntity.GaugingAmount;
                    founderModel.Verified = founderEntity.Verified;
                    founderModel.Live = founderEntity.Live;
                    founderModel.Preview = founderEntity.Preview;
                    founderModel.Comment = founderEntity.Comment;
                    founderModel.UserId = founderEntity.UserId;
                    var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == founderModel.UserId && x.IsActive == true);
                    if (startupEntity != null)
                    {
                        var startupModel = new StartupDeatailModelList();
                        startupModel.StartupId = startupEntity.StartupId;
                        startupModel.StartUpName = startupEntity.StartUpName;
                        startupModel.Address = startupEntity.Address;
                        startupModel.CountryId = startupEntity.CountryId;
                        startupModel.Address = startupEntity.Address;
                        startupModel.StateId = startupEntity.StateId;
                        startupModel.CityId = startupEntity.CityId;
                        startupModel.FoundingYear = startupEntity.FoundingYear;
                        startupModel.CompanyDescription = startupEntity.CompanyDescription;
                        startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                        startupModel.LogoFileName = startupEntity.LogoFileName;
                        startupModel.Logo = startupEntity.Logo;
                        startupModel.EmployeeCount = startupEntity.EmployeeCount;
                        startupModel.SectorId = startupEntity.SectorId;
                        startupModel.SectorName = startupEntity.Sector.SectorName;
                        startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                        startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                        startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                        startupModel.CompanyContact = startupEntity.CompanyContact;
                        startupModel.ServiceDescription = startupEntity.ServiceDescription;
                        startupModel.BusinessModel = startupEntity.BusinessModel;
                        startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                        startupModel.TargetMarket = startupEntity.TargetMarket;
                        startupModel.ManagementInfo = startupEntity.ManagementInfo;
                        startupModel.IsStealth = startupEntity.IsStealth;
                        startupModel.IsActive = startupEntity.IsActive;
                        founderModel.StartupDeatailModel = startupModel;
                    }
                    var founderListEntity = _startupContext.FounderDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).ToList();
                    List<FounderDeatailModel> founderList = new List<FounderDeatailModel>();
                    foreach (var founder in founderListEntity)
                    {
                        var model = new FounderDeatailModel();
                        model.FounderId = founder.FounderId;
                        model.FirstName = founder.FirstName;
                        model.LastName = founder.LastName;
                        model.EmailId = founder.EmailId;
                        model.MobileNo = founder.MobileNo;
                        model.Gender = founder.Gender;
                        model.Description = founder.Description;
                        founderList.Add(model);
                    }
                    founderModel.FounderDeatail = founderList;
                    var fundingListEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
                    if (fundingListEntity != null)
                    {
                        var model = new FundingDetailsModel();
                        model.FundingDetailsId = fundingListEntity.FundingDetailsId;
                        model.FundingId = fundingListEntity.FundingId;
                        model.SeriesName = fundingListEntity.Funding.Name;
                        model.ShareClass = fundingListEntity.ShareClass;
                        model.DateFinancing = fundingListEntity.DateFinancing;
                        model.SharesOutstanding = fundingListEntity.SharesOutstanding;
                        model.IssuePrice = fundingListEntity.IssuePrice;
                        model.ConversionPrice = fundingListEntity.ConversionPrice;
                        model.TotalFinancingSize = fundingListEntity.TotalFinancingSize;
                        model.LiquidityRank = fundingListEntity.LiquidityRank;
                        model.LiquidationPreference = fundingListEntity.LiquidationPreference;
                        model.DividendRate = fundingListEntity.DividendRate;
                        model.DividendType = fundingListEntity.DividendType;
                        model.VotesPerShare = fundingListEntity.VotesPerShare;
                        model.RedemptionRights = fundingListEntity.RedemptionRights;
                        model.ConvertibleToOnPublicListing = fundingListEntity.ConvertibleToOnPublicListing;
                        model.ParticipatingPreferred = fundingListEntity.ParticipatingPreferred;
                        model.QualifiedIpo = fundingListEntity.QualifiedIpo;
                        model.OtherKeyProvisions = fundingListEntity.OtherKeyProvisions;
                        founderModel.FundingModelDetails = model;
                    }
                    investmentModel.founderModelDetails = founderModel;
                }
                investmentModelList.Add(investmentModel);
            }
            return investmentModelList;
        }
        /// <summary>
        /// Get the Invested details set as WatchList
        /// </summary>
        /// <param name="onWatch"></param>
        /// <param name="investorUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<InvestorInvestmentList> GetAllInvestmentOnWatch(bool onWatch, long investorUserId, ref ErrorResponseModel errorResponseModel)
        {
            List<InvestorInvestmentList> investmentModelList = new List<InvestorInvestmentList>();
            var investmentEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.InvestorUserId == investorUserId && x.OnWatchList == onWatch && x.IsActive == true).ToList();
            foreach (var item in investmentEntity)
            {
                var investmentModel = new InvestorInvestmentList();
                investmentModel.InvestorInvestmentId = item.InvestorInvestmentId;
                investmentModel.InvestmentAmount = item.InvestmentAmount;
                investmentModel.InvestmentRound = item.InvestmentRound;
                investmentModel.IndicateInterest = item.IndicateInterest;
                investmentModel.FounderVerifyId = item.FounderVerifyId;
                investmentModel.InvestorUserId = item.InvestorUserId;
                investmentModel.IsActive = item.IsActive;
                investmentModel.OnWatchList = item.OnWatchList;
                investmentModel.CretedDate = item.CreatedDate;
                var founderEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.FounderVerifyId == item.FounderVerifyId && x.SendForVerification == true && ((x.Live == true && x.Preview == false) || (x.Live == false && x.Preview == true)) && x.Verified == true && x.IsActive == true);
                if (founderEntity != null)
                {
                    var founderModel = new FounderModelDetails();
                    founderModel.FounderVerifyId = founderEntity.FounderVerifyId;
                    founderModel.GaugingAmount = founderEntity.GaugingAmount;
                    founderModel.Verified = founderEntity.Verified;
                    founderModel.Live = founderEntity.Live;
                    founderModel.Preview = founderEntity.Preview;
                    founderModel.Comment = founderEntity.Comment;
                    founderModel.UserId = founderEntity.UserId;
                    var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == founderModel.UserId && x.IsActive == true);
                    if (startupEntity != null)
                    {
                        var startupModel = new StartupDeatailModelList();
                        startupModel.StartupId = startupEntity.StartupId;
                        startupModel.StartUpName = startupEntity.StartUpName;
                        startupModel.Address = startupEntity.Address;
                        startupModel.CountryId = startupEntity.CountryId;
                        startupModel.Address = startupEntity.Address;
                        startupModel.StateId = startupEntity.StateId;
                        startupModel.CityId = startupEntity.CityId;
                        startupModel.FoundingYear = startupEntity.FoundingYear;
                        startupModel.CompanyDescription = startupEntity.CompanyDescription;
                        startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                        startupModel.LogoFileName = startupEntity.LogoFileName;
                        startupModel.Logo = startupEntity.Logo;
                        startupModel.EmployeeCount = startupEntity.EmployeeCount;
                        startupModel.SectorId = startupEntity.SectorId;
                        startupModel.SectorName = startupEntity.Sector.SectorName;
                        startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                        startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                        startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                        startupModel.CompanyContact = startupEntity.CompanyContact;
                        startupModel.ServiceDescription = startupEntity.ServiceDescription;
                        startupModel.BusinessModel = startupEntity.BusinessModel;
                        startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                        startupModel.TargetMarket = startupEntity.TargetMarket;
                        startupModel.ManagementInfo = startupEntity.ManagementInfo;
                        startupModel.IsStealth = startupEntity.IsStealth;
                        startupModel.IsActive = startupEntity.IsActive;
                        founderModel.StartupDeatailModel = startupModel;
                    }
                    var founderListEntity = _startupContext.FounderDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).ToList();
                    List<FounderDeatailModel> founderList = new List<FounderDeatailModel>();
                    foreach (var founder in founderListEntity)
                    {
                        var model = new FounderDeatailModel();
                        model.FounderId = founder.FounderId;
                        model.FirstName = founder.FirstName;
                        model.LastName = founder.LastName;
                        model.EmailId = founder.EmailId;
                        model.MobileNo = founder.MobileNo;
                        model.Gender = founder.Gender;
                        model.Description = founder.Description;
                        founderList.Add(model);
                    }
                    founderModel.FounderDeatail = founderList;
                    var fundingListEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
                    if (fundingListEntity != null)
                    {
                        var model = new FundingDetailsModel();
                        model.FundingDetailsId = fundingListEntity.FundingDetailsId;
                        model.FundingId = fundingListEntity.FundingId;
                        model.SeriesName = fundingListEntity.Funding.Name;
                        model.ShareClass = fundingListEntity.ShareClass;
                        model.DateFinancing = fundingListEntity.DateFinancing;
                        model.SharesOutstanding = fundingListEntity.SharesOutstanding;
                        model.IssuePrice = fundingListEntity.IssuePrice;
                        model.ConversionPrice = fundingListEntity.ConversionPrice;
                        model.TotalFinancingSize = fundingListEntity.TotalFinancingSize;
                        model.LiquidityRank = fundingListEntity.LiquidityRank;
                        model.LiquidationPreference = fundingListEntity.LiquidationPreference;
                        model.DividendRate = fundingListEntity.DividendRate;
                        model.DividendType = fundingListEntity.DividendType;
                        model.VotesPerShare = fundingListEntity.VotesPerShare;
                        model.RedemptionRights = fundingListEntity.RedemptionRights;
                        model.ConvertibleToOnPublicListing = fundingListEntity.ConvertibleToOnPublicListing;
                        model.ParticipatingPreferred = fundingListEntity.ParticipatingPreferred;
                        model.QualifiedIpo = fundingListEntity.QualifiedIpo;
                        model.OtherKeyProvisions = fundingListEntity.OtherKeyProvisions;
                        founderModel.FundingModelDetails = model;
                    }
                    founderModel.LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                    double SharesOutstandingSum = _startupContext.FundingDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                    founderModel.LastValuation = Convert.ToDouble(founderModel.LastRoundPrice) * SharesOutstandingSum;
                    investmentModel.founderModelDetails = founderModel;

                }
                investmentModelList.Add(investmentModel);
            }
            return investmentModelList;
        }
        /// <summary>
        /// Add Indicate Interest of Investor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddIndicateInterest(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var investmentEntity = new InvestorInvestmentDetail();
            investmentEntity.InvestorUserId = model.InvestorUserId;
            investmentEntity.IndicateInterest = model.IndicateInterest;
            investmentEntity.InvestmentAmount = model.InvestmentAmount;
            investmentEntity.InvestmentRound = model.InvestmentRound;
            investmentEntity.FounderVerifyId = model.FounderVerifyId;
            investmentEntity.OnWatchList = false;
            investmentEntity.CreatedDate = DateTime.Now;
            investmentEntity.CreatedBy = model.LoggedUserId;
            investmentEntity.IsActive = true;

            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.LoggedUserId);
            var startupEntity = (from founderEntity in _startupContext.FounderVerificationDetails
                                 join founderuser in _startupContext.UserMasters on founderEntity.UserId equals founderuser.UserId
                                 join startup in _startupContext.StartUpDetails on founderuser.UserId equals startup.UserId
                                 where founderEntity.FounderVerifyId == model.FounderVerifyId
                                 select new
                                 {
                                     startup.StartUpName
                                 }).FirstOrDefault();
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<body>");
            strBody.Append("<P>StartupX</P>");
            strBody.Append("<h2>You have shown the interest in the" + " " + startupEntity.StartUpName + "</h2>");
            strBody.Append("</body>");
            var emailSenderModel = new EmailModel();
            emailSenderModel.ToAddress = userEntity.EmailId;
            emailSenderModel.Body = strBody.ToString();
            emailSenderModel.isHtml = true;
            emailSenderModel.Subject = GlobalConstants.InvestorIndicated;
            emailSenderModel.sentStatus = true;
            if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
            {
                _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
            }
            _startupContext.InvestorInvestmentDetails.Add(investmentEntity);
            _startupContext.SaveChanges();

            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Add Indicate Interest";
            userAuditLog.Description = "Indicate Interest Added";
            userAuditLog.UserId = model.LoggedUserId;
            userAuditLog.CreatedBy = model.LoggedUserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);

            return message;
        }
        /// <summary>
        /// Add Investor Investment
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddInvestorInvestment(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var investmentEntity = new InvestorInvestmentDetail();
            investmentEntity.InvestorUserId = model.InvestorUserId;
            investmentEntity.IndicateInterest = model.IndicateInterest;
            investmentEntity.InvestmentAmount = model.InvestmentAmount;
            investmentEntity.InvestmentRound = model.InvestmentRound;
            investmentEntity.FounderVerifyId = model.FounderVerifyId;
            investmentEntity.OnWatchList = false;
            investmentEntity.CreatedDate = DateTime.Now;
            investmentEntity.CreatedBy = model.LoggedUserId;
            investmentEntity.IsActive = true;

            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.LoggedUserId);
            var startupEntity = (from founderEntity in _startupContext.FounderVerificationDetails
                                 join founderuser in _startupContext.UserMasters on founderEntity.UserId equals founderuser.UserId
                                 join startup in _startupContext.StartUpDetails on founderuser.UserId equals startup.UserId
                                 where founderEntity.FounderVerifyId == model.FounderVerifyId
                                 select new
                                 {
                                     startup.StartUpName
                                 }).FirstOrDefault();
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<body>");
            strBody.Append("<P>StartupX</P>");
            strBody.Append("<h2>You have invested the" + " " + model.InvestmentAmount + " " + "amount in the" + " " + startupEntity.StartUpName + " " + "company </h2>");
            strBody.Append("</body>");
            var emailSenderModel = new EmailModel();
            emailSenderModel.ToAddress = userEntity.EmailId;
            emailSenderModel.Body = strBody.ToString();
            emailSenderModel.isHtml = true;
            emailSenderModel.Subject = GlobalConstants.InvestorInvested;
            emailSenderModel.sentStatus = true;
            if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
            {
                _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
            }

            _startupContext.InvestorInvestmentDetails.Add(investmentEntity);
            _startupContext.SaveChanges();
            message = GlobalConstants.InvestorInvested;
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Add Investor Investment";
            userAuditLog.Description = "Investor Investment Added";
            userAuditLog.UserId = model.LoggedUserId;
            userAuditLog.CreatedBy = model.LoggedUserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);

            return message;
        }
        /// <summary>
        /// Add Comapny as On Watch
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddOnWatch(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel)
        {

            var message = string.Empty;
            var existingRecord = _startupContext.InvestorInvestmentDetails.Any(x => x.FounderVerifyId == model.FounderVerifyId && x.InvestorUserId == model.InvestorUserId && x.OnWatchList == true && x.IsActive == true);
            if (!existingRecord)
            {
                var investmentEntity = new InvestorInvestmentDetail();
                investmentEntity.InvestorUserId = model.InvestorUserId;
                investmentEntity.IndicateInterest = model.IndicateInterest;
                investmentEntity.InvestmentAmount = model.InvestmentAmount;
                investmentEntity.InvestmentRound = model.InvestmentRound;
                investmentEntity.FounderVerifyId = model.FounderVerifyId;
                investmentEntity.OnWatchList = model.OnWatchList;
                investmentEntity.CreatedDate = DateTime.Now;
                investmentEntity.CreatedBy = model.LoggedUserId;
                investmentEntity.IsActive = true;

                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.LoggedUserId);
                var startupEntity = (from founderEntity in _startupContext.FounderVerificationDetails
                                     join founderuser in _startupContext.UserMasters on founderEntity.UserId equals founderuser.UserId
                                     join startup in _startupContext.StartUpDetails on founderuser.UserId equals startup.UserId
                                     where founderEntity.FounderVerifyId == model.FounderVerifyId
                                     select new
                                     {
                                         startup.StartUpName
                                     }).FirstOrDefault();
                StringBuilder strBody = new StringBuilder();
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>You have added" + " " + startupEntity.StartUpName + " " + "company in your watch list</h2>");
                strBody.Append("</body>");
                var emailSenderModel = new EmailModel();
                emailSenderModel.ToAddress = userEntity.EmailId;
                emailSenderModel.Body = strBody.ToString();
                emailSenderModel.isHtml = true;
                emailSenderModel.Subject = GlobalConstants.InvestorSetFounderOnWatch;
                emailSenderModel.sentStatus = true;
                if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                {
                    _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                }

                _startupContext.InvestorInvestmentDetails.Add(investmentEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.InvestorSetFounderOnWatch;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add On Watch";
                userAuditLog.Description = "Wathclist Added";
                userAuditLog.UserId = model.LoggedUserId;
                userAuditLog.CreatedBy = model.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;
        }

        /// <summary>
        /// Add to Request Offering
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddRequestOffering(InvestorInvestmentModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existingRecord = _startupContext.InvestorInvestmentDetails.Any(x => x.InvestorUserId == model.InvestorUserId && x.RequestOffering == true && x.IsActive == true);
            if (!existingRecord)
            {
                var investmentEntity = new InvestorInvestmentDetail();
                investmentEntity.InvestorUserId = model.InvestorUserId;
                investmentEntity.IndicateInterest = model.IndicateInterest;
                investmentEntity.InvestmentAmount = model.InvestmentAmount;
                investmentEntity.InvestmentRound = model.InvestmentRound;
                investmentEntity.FounderVerifyId = model.FounderVerifyId;
                investmentEntity.OnWatchList = model.OnWatchList;
                investmentEntity.RequestOffering = true;
                investmentEntity.CreatedDate = DateTime.Now;
                investmentEntity.CreatedBy = model.LoggedUserId;
                investmentEntity.IsActive = true;

                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.LoggedUserId);
                var startupEntity = (from founderEntity in _startupContext.FounderVerificationDetails
                                     join founderuser in _startupContext.UserMasters on founderEntity.UserId equals founderuser.UserId
                                     join startup in _startupContext.StartUpDetails on founderuser.UserId equals startup.UserId
                                     where founderEntity.FounderVerifyId == model.FounderVerifyId
                                     select new
                                     {
                                         startup.StartUpName
                                     }).FirstOrDefault();
                StringBuilder strBody = new StringBuilder();
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>You have shown requestoffering in the" + " " + startupEntity.StartUpName + " " + "company</h2>");
                strBody.Append("</body>");
                var emailSenderModel = new EmailModel();
                emailSenderModel.ToAddress = userEntity.EmailId;
                emailSenderModel.Body = strBody.ToString();
                emailSenderModel.isHtml = true;
                emailSenderModel.Subject = GlobalConstants.RequestOfferingMessage;
                emailSenderModel.sentStatus = true;
                if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                {
                    _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                }

                _startupContext.InvestorInvestmentDetails.Add(investmentEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RequestOfferingMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Request Offering";
                userAuditLog.Description = "Request Offering Added";
                userAuditLog.UserId = model.LoggedUserId;
                userAuditLog.CreatedBy = model.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }

            return message;
        }

        /// <summary>
        /// Delete from watch list
        /// </summary>
        /// <param name="investorInvestmentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteFromWatchlist(int investorInvestmentId, int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var investorWatchEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.InvestorInvestmentId == investorInvestmentId && x.IsActive == true).FirstOrDefault();
            if (investorWatchEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                investorWatchEntity.OnWatchList = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete from Watchlist";
                userAuditLog.Description = "Delete from Watchlist";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
    }
}
