using Microsoft.EntityFrameworkCore;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System.Text;

namespace StartUpX.Business.Implementation
{
    public class InvestorVerificationService : IInvestorVerificationService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;
        private readonly INotificationService _notificationService;
        public InvestorVerificationService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IUserAuditLogService userAuditLogService, INotificationService notificationService)
        {
            _startupContext = startUpDBContext;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;
            _notificationService = notificationService;
        }
        /// <summary>
        /// Get All investor details
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<InvestorModel> GetAllInvestorDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<InvestorModel> investormodelList = new List<InvestorModel>();
            var investorEntity = _startupContext.InvestorVerificationDetails.Where(x => x.SendForVerification == true && x.IsActive == true).ToList();
            foreach (var item in investorEntity)
            {
                var investorModel = new InvestorModel();
                investorModel.InvestorVerifyId = item.InvestorVerifyId;
                investorModel.Verified = item.Verified;
                investorModel.IsActive = item.IsActive;
                investorModel.UserId = item.UserId;
                investorModel.SendForVerification = item.SendForVerification;
                var investordetailEntity = _startupContext.InvestorDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (investordetailEntity != null)
                {
                    var investordetailModel = new InvestorDetailModelList();
                    investordetailModel.InvestorId = investordetailEntity.InvestorId;
                    investordetailModel.ProfileType = investordetailEntity.ProfileType;
                    investordetailModel.FirstName = investordetailEntity.FirstName;
                    investordetailModel.LastName = investordetailEntity.LastName;
                    investordetailModel.EmailId = investordetailEntity.EmailId;
                    investordetailModel.MobileNo = investordetailEntity.MobileNo;
                    investordetailModel.FounderTypeId = investordetailEntity.FounderTypeId;
                    investordetailModel.CountryId = investordetailEntity.CountryId;
                    investordetailModel.StateId = investordetailEntity.StateId;
                    investordetailModel.CityId = investordetailEntity.CityId;
                    investordetailModel.ZipCode = investordetailEntity.ZipCode;
                    investordetailModel.Address1 = investordetailEntity.Address1;
                    investordetailModel.Address2 = investordetailEntity.Address2;
                    investordetailModel.IsActive = investordetailEntity.IsActive;
                    investorModel.InvestorDetailModel = investordetailModel;
                }
                var investmentEntity = _startupContext.InvestmentDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (investmentEntity != null)
                {
                    var model = new InvestmentDetailModel();
                    model.InvestmentId = investmentEntity.InvestmentId;
                    model.InvestmentStage = investmentEntity.InvestmentStage;
                    model.InvestmentSector = investmentEntity.InvestmentSector;
                    model.InvestmentAmount = investmentEntity.InvestmentAmount;
                    investorModel.investmentDeatail = model;
                }
                investormodelList.Add(investorModel);
            }
            return investormodelList;

        }
        /// <summary>
        /// Get All investor details by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="investorVerifyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestorModel GetAllInvestorDetailsById(long userId, long investorVerifyId, ref ErrorResponseModel errorResponseModel)
        {
            var investorModel = new InvestorModel();
            var investorEntity = _startupContext.InvestorVerificationDetails.FirstOrDefault(x => x.SendForVerification == true && x.InvestorVerifyId == investorVerifyId && x.UserId == userId && x.IsActive == true);
            if (investorEntity != null)
            {
                investorModel.SendForVerification = investorEntity.SendForVerification;
                investorModel.InvestorVerifyId = investorEntity.InvestorVerifyId;
                investorModel.Verified = investorEntity.Verified;
                investorModel.IsActive = investorEntity.IsActive;
                investorModel.UserId = investorEntity.UserId;
                var investordetailEntity = _startupContext.InvestorDetails.FirstOrDefault(x => x.UserId == investorEntity.UserId && x.IsActive == true);
                if (investordetailEntity != null)
                {
                    var investordetailModel = new InvestorDetailModelList();
                    investordetailModel.InvestorId = investordetailEntity.InvestorId;
                    investordetailModel.ProfileType = investordetailEntity.ProfileType;
                    investordetailModel.FirstName = investordetailEntity.FirstName;
                    investordetailModel.LastName = investordetailEntity.LastName;
                    investordetailModel.EmailId = investordetailEntity.EmailId;
                    investordetailModel.MobileNo = investordetailEntity.MobileNo;
                    investordetailModel.FounderTypeId = investordetailEntity.FounderTypeId;
                    investordetailModel.CountryId = investordetailEntity.CountryId;
                    investordetailModel.StateId = investordetailEntity.StateId;
                    investordetailModel.CityId = investordetailEntity.CityId;
                    investordetailModel.ZipCode = investordetailEntity.ZipCode;
                    investordetailModel.Address1 = investordetailEntity.Address1;
                    investordetailModel.Address2 = investordetailEntity.Address2;
                    investordetailModel.IsActive = investordetailEntity.IsActive;
                    investorModel.InvestorDetailModel = investordetailModel;

                }
                var investmentEntity = _startupContext.InvestmentDetails.FirstOrDefault(x => x.UserId == investorEntity.UserId && x.IsActive == true);
                if (investmentEntity != null)
                {
                    var model = new InvestmentDetailModel();
                    model.InvestmentId = investmentEntity.InvestmentId;
                    model.InvestmentStage = investmentEntity.InvestmentStage;
                    model.InvestmentSector = investmentEntity.InvestmentSector;
                    model.InvestmentAmount = investmentEntity.InvestmentAmount;
                    investorModel.investmentDeatail = model;
                }
                var documentListEntity = _startupContext.FounderInvestorDocuments.Where(x => x.UserId == investorEntity.UserId).ToList();
                List<FounderInvestorDocumentModel> documentList = new List<FounderInvestorDocumentModel>();
                foreach (var document in documentListEntity)
                {
                    var model = new FounderInvestorDocumentModel();
                    model.DocumentId = document.DocumentId;
                    model.DocumentName = document.DocumentName;
                    model.FileName = document.FileName;
                    model.FilePath = document.FilePath;
                    model.UserId = document.UserId;
                    documentList.Add(model);
                }
                investorModel.founderInvestorDocumentList = documentList;
            }
            return investorModel;
        }
        /// <summary>
        /// Get All verified investor details
        /// </summary>
        /// <param name="verified"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<InvestorModel> GetAllVerifiedNonInvestorDetails(bool verified, ref ErrorResponseModel errorResponseModel)
        {
            List<InvestorModel> investorModelList = new List<InvestorModel>();
            var investorEntity = _startupContext.InvestorVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == verified && x.IsActive == true).ToList();
            foreach (var item in investorEntity)
            {
                var investorModel = new InvestorModel();
                investorModel.InvestorVerifyId = item.InvestorVerifyId;
                investorModel.Verified = item.Verified;
                investorModel.IsActive = item.IsActive;
                investorModel.UserId = item.UserId;
                investorModel.SendForVerification = item.SendForVerification;
                var investorDetailEntity = _startupContext.InvestorDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (investorDetailEntity != null)
                {
                    var investordetailModel = new InvestorDetailModelList();
                    investordetailModel.InvestorId = investorDetailEntity.InvestorId;
                    investordetailModel.ProfileType = investorDetailEntity.ProfileType;
                    investordetailModel.FirstName = investorDetailEntity.FirstName;
                    investordetailModel.LastName = investorDetailEntity.LastName;
                    investordetailModel.EmailId = investorDetailEntity.EmailId;
                    investordetailModel.MobileNo = investorDetailEntity.MobileNo;
                    investordetailModel.FounderTypeId = investorDetailEntity.FounderTypeId;
                    investordetailModel.CountryId = investorDetailEntity.CountryId;
                    investordetailModel.StateId = investorDetailEntity.StateId;
                    investordetailModel.CityId = investorDetailEntity.CityId;
                    investordetailModel.ZipCode = investorDetailEntity.ZipCode;
                    investordetailModel.Address1 = investorDetailEntity.Address1;
                    investordetailModel.Address2 = investorDetailEntity.Address2;
                    investordetailModel.IsActive = investorDetailEntity.IsActive;
                    investorModel.InvestorDetailModel = investordetailModel;
                }
                var investmentEntity = _startupContext.InvestmentDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (investmentEntity != null)
                {
                    var model = new InvestmentDetailModel();
                    model.InvestmentId = investmentEntity.InvestmentId;
                    model.InvestmentStage = investmentEntity.InvestmentStage;
                    model.InvestmentSector = investmentEntity.InvestmentSector;
                    model.InvestmentAmount = investmentEntity.InvestmentAmount;
                    investorModel.investmentDeatail = model;
                }
                investorModelList.Add(investorModel);
            }
            return investorModelList;
        }
        /// <summary>
        /// Send for verification investor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SendForVerification(InvestorVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var investorVerification = new InvestorVerificationDetail();
            var existingRecord = _startupContext.InvestorVerificationDetails.Any(x => x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                var verifyEntity = new InvestorVerificationDetail();
                verifyEntity.UserId = model.UserId;
                verifyEntity.SendForVerification = true;
                verifyEntity.CreatedDate = DateTime.Now;
                verifyEntity.CreatedBy = model.LoggedUserId;
                verifyEntity.IsActive = true;
                verifyEntity.Verified = false;
                _startupContext.InvestorVerificationDetails.Add(verifyEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.SentForVerification;
            }
            else
            {
                var verifyEntity = _startupContext.InvestorVerificationDetails.FirstOrDefault(x => x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    verifyEntity.UserId = model.UserId;
                    verifyEntity.SendForVerification = true;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.LoggedUserId;
                    verifyEntity.IsActive = true;
                    verifyEntity.Verified = false;
                    _startupContext.InvestorVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.SentForVerification;
                }
            }
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Send for Verification";
            userAuditLog.Description = "Senf for Verification";
            userAuditLog.UserId = model.UserId;
            userAuditLog.CreatedBy = model.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);

            return message;
        }
        /// <summary>
        /// Approve/Notpprove investor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string VerificationApprove(InvestorVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var investorVerification = new InvestorVerificationDetail();
            var existingRecord = _startupContext.InvestorVerificationDetails.Any(x => x.InvestorVerifyId == model.InvestorVerifyId && x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var verifyEntity = _startupContext.InvestorVerificationDetails.FirstOrDefault(x => x.InvestorVerifyId == model.InvestorVerifyId && x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    var userEnitity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == verifyEntity.UserId);
                    StringBuilder strBody = new StringBuilder();
                    var emailSenderModel = new EmailModel();
                    verifyEntity.UserId = model.UserId;
                    if (model.Verified != true)
                    {
                        verifyEntity.SendForVerification = false;
                        verifyEntity.Comment = model.Comment;
                        message = GlobalConstants.InvestorNotApproved;
                        strBody.Append("<body>");
                        strBody.Append("<P>StartupX</P>");
                        strBody.Append("<h2>Your profile not verified because" + " " + model.Comment + " " + "</h2>");
                        strBody.Append("</body>");
                        emailSenderModel.Subject = GlobalConstants.InvestorNotApproved;
                        /// Notification Add
                        var notification = new NotificationModel();
                        notification.Message = "Your profile not verified because" + " " + model.Comment;
                        notification.UserId = model.UserId;
                        notification.LoggedUserId = model.LoggedUserId;
                        _notificationService.AddNotification(notification);
                    }
                    else
                    {
                        message = GlobalConstants.InvestorApproved;
                        strBody.Append("<body>");
                        strBody.Append("<P>StartupX</P>");
                        strBody.Append("<h2>Your profile verified.</h2>");
                        strBody.Append("</body>");
                        emailSenderModel.Subject = GlobalConstants.InvestorApproved;
                        /// Notification Add
                        var notification = new NotificationModel();
                        notification.Message = "Your profile verified";
                        notification.UserId = model.UserId;
                        notification.LoggedUserId = model.LoggedUserId;
                        _notificationService.AddNotification(notification);
                    }
                    verifyEntity.Verified = model.Verified;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.LoggedUserId;

                    emailSenderModel.ToAddress = userEnitity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }
                    _startupContext.InvestorVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Approve Verification";
                    userAuditLog.Description = "Verification Approved";
                    userAuditLog.UserId = model.UserId;
                    userAuditLog.CreatedBy = model.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }
            return message;
        }

        /// <summary>
        /// Investor Profile completion
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestorVerificationModel InvestorProfileCompletion(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
         
            var investorModel = new InvestorVerificationModel();
            int investorDetailsFound = _startupContext.InvestorDetails.Where(x => x.UserId == userId && x.IsActive == true).Count();
            int suitablityFound = _startupContext.SuitabilityQuestions.Where(x => x.UserId == userId && x.IsActive == true).Count();
            int investmentDetailsFound = _startupContext.InvestmentDetails.Where(x => x.UserId == userId && x.IsActive == true).Count();
            int investmentDocumentFound = _startupContext.FounderInvestorDocuments.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Distinct().Count();
            investorModel.InvestorProfileCompletion = (investorDetailsFound + suitablityFound + investmentDetailsFound + investmentDocumentFound).ToString();
            investorModel.Verified = _startupContext.InvestorVerificationDetails.Any(x => x.UserId == userId && x.Verified == true);
            return investorModel;
        }
        /// <summary>
        /// Investor Status Count
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestorVerificationModel InvestorStatusCount(ref ErrorResponseModel errorResponseModel)
        {
            var investorModel = new InvestorVerificationModel();
            investorModel.VerifiedCount = _startupContext.InvestorVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == true && x.IsActive == true).Count();
            investorModel.NonVerifiedCount = _startupContext.InvestorVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == false && x.IsActive == true).Count();
            investorModel.RequestFundingCount = _startupContext.InvestorInvestmentDetails.Where(x => x.RequestOffering == true && x.IsActive == true).Count();
            investorModel.InvestorsInvestedCount = _startupContext.InvestorInvestmentDetails.Where(x => x.InvestmentAmount!= null&& x.InvestmentAmount != "" && x.IsActive == true).Count();
            investorModel.IndicateInvestmentCount = _startupContext.InvestorInvestmentDetails.Where(x => x.IndicateInterest != null&& x.IndicateInterest != "" && x.IsActive == true).Count();

            return investorModel;
        }

        /// <summary>
        /// Get All live preview startup details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllLivePreviewStartupDetails(bool live, bool preview, ref ErrorResponseModel errorResponseModel)
        {
            var currentdate = DateTime.Now;
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var founderEntity = new List<FounderVerificationDetail>();
            if (live == true && preview == true)
            {
                founderEntity = _startupContext.FounderVerificationDetails.Where(x => (x.SendForVerification == true || x.RaiseFunding == true || x.Verified == true) && ((x.Live == true && x.Preview == false) || (x.Live == false && x.Preview == true) || x.RaiseFunding == true) && x.IsActive == true).ToList();
            }
            else
            {
                founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == live && x.Preview == preview && x.Verified == true && x.RaiseFunding == false && x.IsActive == true).ToList();
            }
            foreach (var item in founderEntity)
            {
                var founderModel = new FounderModelDetails();
                founderModel.FounderVerifyId = item.FounderVerifyId;
                founderModel.GaugingAmount = item.GaugingAmount;
                founderModel.Verified = item.Verified;
                founderModel.Live = item.Live;
                founderModel.Preview = item.Preview;
                founderModel.Comment = item.Comment;
                founderModel.UserId = item.UserId;
                founderModel.RaiseFunding = item.RaiseFunding;
                var daysCount = (currentdate - (DateTime)(item.UpdatedDate == null ? currentdate.AddDays(-16) : item.UpdatedDate)).Days;
                founderModel.NewFounder = daysCount <= 15 ? true : false;
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
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
                    founderModel.StartupName = startupEntity.StartUpName;
                    founderModel.StartupDeatailModel = startupModel;
                }
                var founderListEntity = _startupContext.FounderDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).ToList();
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
                var fundingListEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == item.UserId && x.IsActive == true).ToList();
                List<FundingDetailsModel> fundingList = new List<FundingDetailsModel>();
                foreach (var funding in fundingListEntity)
                {
                    var model = new FundingDetailsModel();
                    model.FundingDetailsId = funding.FundingDetailsId;
                    model.FundingId = funding.FundingId;
                    model.SeriesName = funding.Funding.Name;
                    model.ShareClass = funding.ShareClass;
                    model.DateFinancing = funding.DateFinancing;
                    model.SharesOutstanding = funding.SharesOutstanding;
                    model.IssuePrice = funding.IssuePrice;
                    model.ConversionPrice = funding.ConversionPrice;
                    model.TotalFinancingSize = funding.TotalFinancingSize;
                    model.LiquidityRank = funding.LiquidityRank;
                    model.LiquidationPreference = funding.LiquidationPreference;
                    model.DividendRate = funding.DividendRate;
                    model.DividendType = funding.DividendType;
                    model.VotesPerShare = funding.VotesPerShare;
                    model.RedemptionRights = funding.RedemptionRights;
                    model.ConvertibleToOnPublicListing = funding.ConvertibleToOnPublicListing;
                    model.ParticipatingPreferred = funding.ParticipatingPreferred;
                    model.QualifiedIpo = funding.QualifiedIpo;
                    model.OtherKeyProvisions = funding.OtherKeyProvisions;
                    fundingList.Add(model);
                }
                founderModel.FundingDetails = fundingList;
                var fundingEntity = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
                if (fundingEntity != null)
                {
                    var model = new FundingDetailsModel();
                    model.FundingDetailsId = fundingEntity.FundingDetailsId;
                    model.FundingId = fundingEntity.FundingId;
                    model.SeriesName = fundingEntity.Funding.Name;
                    model.ShareClass = fundingEntity.ShareClass;
                    model.DateFinancing = fundingEntity.DateFinancing;
                    model.SharesOutstanding = fundingEntity.SharesOutstanding;
                    model.IssuePrice = fundingEntity.IssuePrice;
                    model.ConversionPrice = fundingEntity.ConversionPrice;
                    model.TotalFinancingSize = fundingEntity.TotalFinancingSize;
                    model.LiquidityRank = fundingEntity.LiquidityRank;
                    model.LiquidationPreference = fundingEntity.LiquidationPreference;
                    model.DividendRate = fundingEntity.DividendRate;
                    model.DividendType = fundingEntity.DividendType;
                    model.VotesPerShare = fundingEntity.VotesPerShare;
                    model.RedemptionRights = fundingEntity.RedemptionRights;
                    model.ConvertibleToOnPublicListing = fundingEntity.ConvertibleToOnPublicListing;
                    model.ParticipatingPreferred = fundingEntity.ParticipatingPreferred;
                    model.QualifiedIpo = fundingEntity.QualifiedIpo;
                    model.OtherKeyProvisions = fundingEntity.OtherKeyProvisions;
                    founderModel.FundingModelDetails = model;
                }
                var investmentOpportunityEntity = _startupContext.InvestmentOpportunityDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
                if (investmentOpportunityEntity != null)
                {
                    var invOpportunityModel = new InvestmnetopportunityModel();
                    invOpportunityModel.FundName = investmentOpportunityEntity.FundName;
                    invOpportunityModel.FundStrategy = investmentOpportunityEntity.FundStrategy;
                    invOpportunityModel.SalesFee = investmentOpportunityEntity.SalesFee;
                    invOpportunityModel.ExpectedSharePrice = investmentOpportunityEntity.ExpectedSharePrice;
                    invOpportunityModel.SecurityType = investmentOpportunityEntity.SecurityType;
                    invOpportunityModel.ImpliedCompanyValuation = investmentOpportunityEntity.ImpliedCompanyValuation;
                    invOpportunityModel.LatestPostMoneyValuation = investmentOpportunityEntity.LatestPostMoneyValuation;
                    invOpportunityModel.Discount = investmentOpportunityEntity.Discount;
                    invOpportunityModel.MinimumInvestmentSize = investmentOpportunityEntity.MinimumInvestmentSize;
                    founderModel.Investmnetopportunity = invOpportunityModel;
                }
                var investorEntity = _startupContext.InvestorInvestmentDetails.Include(x => x.InvestorUser).Where(x => x.FounderVerifyId == item.FounderVerifyId).ToList();
                List<InvestorInvestmentList> investorList = new List<InvestorInvestmentList>();
                foreach (var item1 in investorEntity)
                {
                    var model = new InvestorInvestmentList();
                    model.InvestorInvestmentId = item1.InvestorInvestmentId;
                    model.InvestmentAmount = item1.InvestmentAmount;
                    model.InvestmentRound = item1.InvestmentRound;
                    model.IndicateInterest = item1.IndicateInterest;
                    model.FounderVerifyId = item1.FounderVerifyId;
                    model.InvestorUserId = item1.InvestorUserId;
                    model.IsActive = item1.IsActive;
                    model.OnWatchList = item1.OnWatchList;
                    model.InvestorName = item1.InvestorUser.FirstName + " " + item1.InvestorUser.LastName;
                    var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == item1.InvestorUserId);
                    var notableInvestorEntity = _startupContext.NotableInvestorMasters.FirstOrDefault(x => x.EmailId == userEntity.EmailId && x.IsActive == true);
                    if (notableInvestorEntity != null)
                    {
                        founderModel.NotableInvestorName = notableInvestorEntity.FirstName + " " + notableInvestorEntity.LastName;
                    }
                    investorList.Add(model);
                }
                founderModel.InvestorInvestmentList = investorList;
                founderModel.LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                double SharesOutstandingSum = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                founderModel.LastValuation = Convert.ToDouble(founderModel.LastRoundPrice) * SharesOutstandingSum;
                founderModel.GaugingPercentage = _startupContext.InvestorInvestmentDetails.Where(x => x.FounderVerifyId == item.FounderVerifyId && x.IsActive == true).Select(t => Convert.ToInt32(t.IndicateInterest)).Sum();
                var LastRoundPrice1 = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Skip(1).Select(x => x.IssuePrice).FirstOrDefault();
                double SharesOutstandingSum1 = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Skip(1).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                founderModel.SecondLastValuation = Convert.ToDouble(LastRoundPrice1) * SharesOutstandingSum1;
                founderModelList.Add(founderModel);


            }
            return founderModelList;
        }

        /// <summary>
        /// Get All Startup By Sector
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllStartupDetailsBySector(long userId, ref ErrorResponseModel errorResponseModel)
        {
            string[] sectorName;
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var investorEntity = _startupContext.InvestmentDetails.FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            if (investorEntity != null)
            {
                sectorName = investorEntity.InvestmentSector.Split(',');
                foreach (var item in sectorName)
                {
                    var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).Where(x => x.Sector.SectorName == item && x.IsActive == true).ToList();
                    foreach (var startup in startupEntity)
                    {
                        var founderVerifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.UserId == startup.UserId);
                        if (founderVerifyEntity != null)
                        {
                            var founderModel = new FounderModelDetails();
                            founderModel.UserId = startup.UserId;
                            founderModel.FounderVerifyId = founderVerifyEntity.FounderVerifyId;
                            var startupModel = new StartupDeatailModelList();
                            startupModel.StartupId = startup.StartupId;
                            startupModel.StartUpName = startup.StartUpName;
                            startupModel.Address = startup.Address;
                            startupModel.CountryId = startup.CountryId;
                            startupModel.Address = startup.Address;
                            startupModel.StateId = startup.StateId;
                            startupModel.CityId = startup.CityId;
                            startupModel.FoundingYear = startup.FoundingYear;
                            startupModel.CompanyDescription = startup.CompanyDescription;
                            startupModel.WebsiteUrl = startup.WebsiteUrl;
                            startupModel.LogoFileName = startup.LogoFileName;
                            startupModel.Logo = startup.Logo;
                            startupModel.EmployeeCount = startup.EmployeeCount;
                            startupModel.SectorId = startup.SectorId;
                            startupModel.SectorName = startup.Sector.SectorName;
                            startupModel.CompanyEmailId = startup.CompanyEmailId;
                            startupModel.CompanyLegalName = startup.CompanyLegalName;
                            startupModel.CompanyHeadquartersAddress = startup.CompanyHeadquartersAddress;
                            startupModel.CompanyContact = startup.CompanyContact;
                            startupModel.ServiceDescription = startup.ServiceDescription;
                            startupModel.BusinessModel = startup.BusinessModel;
                            startupModel.TargetCustomerBase = startup.TargetCustomerBase;
                            startupModel.TargetMarket = startup.TargetMarket;
                            startupModel.ManagementInfo = startup.ManagementInfo;
                            startupModel.IsStealth = startup.IsStealth;
                            startupModel.IsActive = startup.IsActive;
                            founderModel.StartupDeatailModel = startupModel;
                            var founderListEntity = _startupContext.FounderDetails.Where(x => x.UserId == startup.UserId && x.IsActive == true).ToList();
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
                            var fundingListEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == startup.UserId && x.IsActive == true).ToList();
                            List<FundingDetailsModel> fundingList = new List<FundingDetailsModel>();
                            foreach (var funding in fundingListEntity)
                            {
                                var model = new FundingDetailsModel();
                                model.FundingDetailsId = funding.FundingDetailsId;
                                model.FundingId = funding.FundingId;
                                model.SeriesName = funding.Funding.Name;
                                model.ShareClass = funding.ShareClass;
                                model.DateFinancing = funding.DateFinancing;
                                model.SharesOutstanding = funding.SharesOutstanding;
                                model.IssuePrice = funding.IssuePrice;
                                model.ConversionPrice = funding.ConversionPrice;
                                model.TotalFinancingSize = funding.TotalFinancingSize;
                                model.LiquidityRank = funding.LiquidityRank;
                                model.LiquidationPreference = funding.LiquidationPreference;
                                model.DividendRate = funding.DividendRate;
                                model.DividendType = funding.DividendType;
                                model.VotesPerShare = funding.VotesPerShare;
                                model.RedemptionRights = funding.RedemptionRights;
                                model.ConvertibleToOnPublicListing = funding.ConvertibleToOnPublicListing;
                                model.ParticipatingPreferred = funding.ParticipatingPreferred;
                                model.QualifiedIpo = funding.QualifiedIpo;
                                model.OtherKeyProvisions = funding.OtherKeyProvisions;
                                fundingList.Add(model);
                            }
                            founderModel.FundingDetails = fundingList;
                            founderModel.LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == startup.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                            double SharesOutstandingSum = _startupContext.FundingDetails.Where(x => x.UserId == startup.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                            founderModel.LastValuation = Convert.ToDouble(founderModel.LastRoundPrice) * SharesOutstandingSum;
                            founderModelList.Add(founderModel);
                        }
                    }
                }
            }
            return founderModelList;
        }

        public List<RequestOfferModel> GetAllrequestOfferingInvestorDetails(ref ErrorResponseModel errorResponseModel)
        {

            List<RequestOfferModel> founderModelList = new List<RequestOfferModel>();
            var investorEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.RequestOffering == true && x.IsActive == true).ToList();
            foreach (var item in investorEntity)
            {
                var founderModel = new RequestOfferModel();

                //founderModel.StartupDeatailModel = new StartupDeatailModelList();
                 var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == item.InvestorUserId && x.IsActive == true);
                if (startupEntity != null)
                {
                  
                        var startupModel = new StartupDeatailModelList();
                        startupModel.StartupId = startupEntity.StartupId;
                        startupModel.StartUpName = startupEntity.StartUpName;
                        startupModel.Address = startupEntity.Address;
                        startupModel.FoundingYear = startupEntity.FoundingYear;
                        startupModel.LogoFileName = startupEntity.LogoFileName;
                        startupModel.Logo = startupEntity.Logo;
                        startupModel.SectorId = startupEntity.SectorId;
                        startupModel.SectorName = startupEntity.Sector.SectorName;
                        startupModel.IsStealth = startupEntity.IsStealth;
                        startupModel.IsActive = startupEntity.IsActive;
                       
                    founderModel.StartupDeatailModel = startupModel;
                }
                var investorsEntity = _startupContext.InvestorDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investorsEntity != null)
                {
                    founderModel.InvestorName = investorsEntity.FirstName + " " + investorsEntity.LastName;
                    founderModel.investorId = investorsEntity.UserId;

                }

                var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.UserId == item.InvestorUserId && x.IsActive == true).FirstOrDefault();
                if (founderEntity != null)
                {

                    founderModel.Verified = founderEntity.Verified;
                    founderModel.UserId = founderEntity.UserId;
                    founderModel.FounderVerifiedId = founderEntity.FounderVerifyId;

                }

                var investEntity = _startupContext.InvestorVerificationDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investEntity != null)
                {

                    founderModel.InvestorVerifiedId = investEntity.InvestorVerifyId;
                 

                }

                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }

        public List<InvestorsInvestmentsModel> GetAllInvestmentsDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<InvestorsInvestmentsModel> founderModelList = new List<InvestorsInvestmentsModel>();
            var investorEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.InvestmentAmount != null && x.InvestmentAmount !="" && x.IsActive == true).ToList();
            foreach (var item in investorEntity)
            {
                var founderModel = new InvestorsInvestmentsModel();

                //founderModel.StartupDeatailModel = new StartupDeatailModelList();
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == item.InvestorUserId && x.IsActive == true);
                if (startupEntity != null)
                {

                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;

                    founderModel.StartupDeatailModel = startupModel;
                }
                var investorsEntity = _startupContext.InvestorDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investorsEntity != null)
                {
                    founderModel.InvestorName = investorsEntity.FirstName + " " + investorsEntity.LastName;
                    founderModel.investorId = investorsEntity.UserId;

                }

                var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.UserId == item.InvestorUserId && x.IsActive == true).FirstOrDefault();
                if (founderEntity != null)
                {

                    founderModel.Verified = founderEntity.Verified;
                    founderModel.UserId = founderEntity.UserId;
                    founderModel.FounderVerifiedId = founderEntity.FounderVerifyId;

                }

                var investEntity = _startupContext.InvestorVerificationDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investEntity != null)
                {

                    founderModel.InvestorVerifiedId = investEntity.InvestorVerifyId;


                }

                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }

        public List<IndicateInterestModel> GetAllIndicateInvestmentsDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<IndicateInterestModel> founderModelList = new List<IndicateInterestModel>();
            var investorEntity = _startupContext.InvestorInvestmentDetails.Where(x => x.IndicateInterest != null && x.IndicateInterest != "" && x.IsActive == true).ToList();
           

            foreach (var item in investorEntity)
            {
                var founderModel = new IndicateInterestModel();
                if (investorEntity != null)
                {
                    founderModel.IndicateInterest = item.IndicateInterest;
                }

                //founderModel.StartupDeatailModel = new StartupDeatailModelList();
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == item.InvestorUserId && x.IsActive == true);
                if (startupEntity != null)
                {

                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;

                    founderModel.StartupDeatailModel = startupModel;
                }
                var investorsEntity = _startupContext.InvestorDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investorsEntity != null)
                {
                    founderModel.InvestorName = investorsEntity.FirstName + " " + investorsEntity.LastName;
                    founderModel.investorId = investorsEntity.UserId;

                }

                var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.UserId == item.InvestorUserId && x.IsActive == true).FirstOrDefault();
                if (founderEntity != null)
                {

                    founderModel.Verified = founderEntity.Verified;
                    founderModel.UserId = founderEntity.UserId;
                    founderModel.FounderVerifiedId = founderEntity.FounderVerifyId;

                }

                var investEntity = _startupContext.InvestorVerificationDetails.Where(x => x.UserId == item.CreatedBy && x.IsActive == true).FirstOrDefault();
                if (investEntity != null)
                {

                    founderModel.InvestorVerifiedId = investEntity.InvestorVerifyId;


                }

                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }
    }
}
