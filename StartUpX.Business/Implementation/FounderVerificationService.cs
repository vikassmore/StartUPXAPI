using Microsoft.EntityFrameworkCore;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class FounderVerificationService : IFounderVerificationService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;
        private readonly INotificationService _notificationService;
        public FounderVerificationService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IUserAuditLogService userAuditLogService, INotificationService notificationService)
        {
            _startupContext = startUpDBContext;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllFounderDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.IsActive == true).ToList();
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
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
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

               
                    founderModelList.Add(founderModel);
            }
            return founderModelList;
        }
        /// <summary>
        /// Get All verified founder details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllVerifiedNonFounderDetails(bool verified, ref ErrorResponseModel errorResponseModel)
        {
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == verified && x.IsActive == true).ToList();
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
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
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
                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }
        /// <summary>
        /// Get All live preview founder details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllLivePreviewFounderDetails(bool live, bool preview, ref ErrorResponseModel errorResponseModel)
        {
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == live && (x.Preview == preview || x.Preview == true) && x.Verified == true && x.IsActive == true).ToList();
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
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
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
                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }
        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        public FounderModelDetails GetAllFounderDetailsById(long userId, long founderVerifyId, ref ErrorResponseModel errorResponseModel)
        {
            var founderModel = new FounderModelDetails();
            var founderEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => (x.SendForVerification == true || x.SendForVerification == null) && x.FounderVerifyId == founderVerifyId && x.UserId == userId && x.IsActive == true);
            if (founderEntity != null)
            {
                founderModel.FounderVerifyId = founderEntity.FounderVerifyId;
                founderModel.GaugingAmount = founderEntity.GaugingAmount;
                founderModel.Verified = founderEntity.Verified;
                founderModel.Live = founderEntity.Live;
                founderModel.Preview = founderEntity.Preview;
                founderModel.Comment = founderEntity.Comment;
                founderModel.UserId = founderEntity.UserId;
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == founderEntity.UserId && x.IsActive == true);
                if (startupEntity != null)
                {
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CountryId = startupEntity.CountryId;
                    startupModel.Address = startupEntity.Address;
                    startupModel.StateId = startupEntity.StateId;
                    startupModel.CityId = startupEntity.CityId;
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
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
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
                var fundingListEntity = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).ToList();
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
                var fundingEntity = _startupContext.FundingDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
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
                var documentListEntity = _startupContext.FounderInvestorDocuments.Where(x => x.UserId == founderEntity.UserId).ToList();
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
                founderModel.founderInvestorDocumentList = documentList;
                founderModel.LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                double SharesOutstandingSum = _startupContext.FundingDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                founderModel.LastValuation = Convert.ToDouble(founderModel.LastRoundPrice) * SharesOutstandingSum;
                founderModel.GaugingPercentage = _startupContext.InvestorInvestmentDetails.Where(x => x.FounderVerifyId == founderEntity.FounderVerifyId).Select(t => Convert.ToInt32(t.IndicateInterest)).Sum();
                var investmentOpportunityEntity = _startupContext.InvestmentOpportunityDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).FirstOrDefault();
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
                var invOpportunityEntity = _startupContext.InvestmentOpportunityDetails.Where(x => x.UserId == founderEntity.UserId && x.IsActive == true).ToList();
                List<InvestmnetopportunityModel> investorOppList = new List<InvestmnetopportunityModel>();
                foreach (var item in invOpportunityEntity)
                {
                    var invOpportunityModel = new InvestmnetopportunityModel();
                    invOpportunityModel.FundName = item.FundName;
                    invOpportunityModel.FundStrategy = item.FundStrategy;
                    invOpportunityModel.SalesFee = item.SalesFee;
                    invOpportunityModel.ExpectedSharePrice = item.ExpectedSharePrice;
                    invOpportunityModel.SecurityType = item.SecurityType;
                    invOpportunityModel.ImpliedCompanyValuation = item.ImpliedCompanyValuation;
                    invOpportunityModel.LatestPostMoneyValuation = item.LatestPostMoneyValuation;
                    invOpportunityModel.Discount = item.Discount;
                    invOpportunityModel.MinimumInvestmentSize = item.MinimumInvestmentSize;
                    investorOppList.Add(invOpportunityModel);
                }
                founderModel.InvestmnetopportunityList = investorOppList;
                var investorEntity = _startupContext.InvestorInvestmentDetails.Include(x => x.InvestorUser).Where(x => x.FounderVerifyId == founderEntity.FounderVerifyId).ToList();
                List<InvestorInvestmentList> investorList = new List<InvestorInvestmentList>();
                foreach (var item in investorEntity)
                {
                    var model = new InvestorInvestmentList();
                    model.InvestorInvestmentId = item.InvestorInvestmentId;
                    model.InvestmentAmount = item.InvestmentAmount;
                    model.InvestmentRound = item.InvestmentRound;
                    model.IndicateInterest = item.IndicateInterest;
                    model.FounderVerifyId = item.FounderVerifyId;
                    model.InvestorUserId = item.InvestorUserId;
                    model.IsActive = item.IsActive;
                    model.OnWatchList = item.OnWatchList;
                    model.InvestorName = item.InvestorUser.FirstName + " " + item.InvestorUser.LastName;
                    var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == item.InvestorUserId);
                    var notableInvestorEntity = _startupContext.NotableInvestorMasters.FirstOrDefault(x => x.EmailId == userEntity.EmailId && x.IsActive == true);
                    if (notableInvestorEntity != null)
                    {
                        founderModel.NotableInvestorName = notableInvestorEntity.FirstName + " " + notableInvestorEntity.LastName;
                    }
                    investorList.Add(model);
                }
                founderModel.InvestorInvestmentList = investorList;


                List<CompitetorFounders> compitetorModelList = new List<CompitetorFounders>();
                var compitetorEnitity = _startupContext.StartUpDetails.Include(x => x.Sector).Where(x => x.SectorId == startupEntity.SectorId && x.UserId != founderEntity.UserId).ToList();
                foreach (var item in compitetorEnitity)
                {
                    var compitetorModel = new CompitetorFounders();
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = item.StartupId;
                    startupModel.StartUpName = item.StartUpName;
                    startupModel.Address = item.Address;
                    startupModel.CountryId = item.CountryId;
                    startupModel.Address = item.Address;
                    startupModel.StateId = item.StateId;
                    startupModel.CityId = item.CityId;
                    startupModel.FoundingYear = item.FoundingYear;
                    startupModel.CompanyDescription = item.CompanyDescription;
                    startupModel.WebsiteUrl = item.WebsiteUrl;
                    startupModel.LogoFileName = item.LogoFileName;
                    startupModel.Logo = item.Logo;
                    startupModel.EmployeeCount = item.EmployeeCount;
                    startupModel.SectorId = item.SectorId;
                    startupModel.SectorName = item.Sector.SectorName;
                    startupModel.CompanyEmailId = item.CompanyEmailId;
                    startupModel.CompanyLegalName = item.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = item.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = item.CompanyContact;
                    startupModel.ServiceDescription = item.ServiceDescription;
                    startupModel.BusinessModel = item.BusinessModel;
                    startupModel.TargetCustomerBase = item.TargetCustomerBase;
                    startupModel.TargetMarket = item.TargetMarket;
                    startupModel.ManagementInfo = item.ManagementInfo;
                    startupModel.IsStealth = item.IsStealth;
                    startupModel.IsActive = item.IsActive;
                    compitetorModel.StartupDeatailModel = startupModel;
                    var founderListEntity1 = _startupContext.FounderDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).ToList();
                    List<FounderDeatailModel> founderList1 = new List<FounderDeatailModel>();
                    foreach (var founder in founderListEntity1)
                    {
                        var model = new FounderDeatailModel();
                        model.FounderId = founder.FounderId;
                        model.FirstName = founder.FirstName;
                        model.LastName = founder.LastName;
                        model.EmailId = founder.EmailId;
                        model.MobileNo = founder.MobileNo;
                        model.Gender = founder.Gender;
                        model.Description = founder.Description;
                        founderList1.Add(model);
                    }
                    compitetorModel.FounderDeatail = founderList1;
                    var fundingListEntity1 = _startupContext.FundingDetails.Include(x => x.Funding).Where(x => x.UserId == item.UserId && x.IsActive == true).ToList();
                    List<FundingDetailsModel> fundingList1 = new List<FundingDetailsModel>();
                    foreach (var funding in fundingListEntity1)
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
                        fundingList1.Add(model);
                    }
                    compitetorModel.FundingDetails = fundingList1;
                    compitetorModel.LastRoundPrice = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).Select(x => x.IssuePrice).FirstOrDefault();
                    double SharesOutstandingSum1 = _startupContext.FundingDetails.Where(x => x.UserId == item.UserId && x.IsActive == true).Select(t => Convert.ToDouble(t.SharesOutstanding)).Sum();
                    compitetorModel.LastValuation = Convert.ToDouble(compitetorModel.LastRoundPrice) * SharesOutstandingSum1;
                    // compitetorModel.GaugingPercentage = _startupContext.InvestorInvestmentDetails.Where(x => x.FounderVerifyId == founderEntity.FounderVerifyId).Select(t => Convert.ToInt32(t.IndicateInterest)).Sum();
                    compitetorModelList.Add(compitetorModel);
                }
                founderModel.compitetorFounders = compitetorModelList;
            }
            return founderModel;
        }
        /// <summary>
        /// Get All IsStealth founder details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllFounderIsStealthDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();

            var founderEntity = _startupContext.StartUpDetails.Include(x => x.Sector).Where(x => x.IsStealth == true && x.IsActive == true).ToList();
            foreach (var item in founderEntity)
            {
                var founderModel = new FounderModelDetails();
                founderModel.Verified = false;
                founderModel.Live = false;
                founderModel.Preview = false;
                founderModel.UserId = item.UserId;
                var startupModel = new StartupDeatailModelList();
                startupModel.StartupId = item.StartupId;
                startupModel.StartUpName = item.StartUpName;
                startupModel.Address = item.Address;
                startupModel.CountryId = item.CountryId;
                startupModel.Address = item.Address;
                startupModel.StateId = item.StateId;
                startupModel.CityId = item.CityId;
                startupModel.CompanyEmailId = item.CompanyEmailId;
                startupModel.CompanyDescription = item.CompanyDescription;
                startupModel.WebsiteUrl = item.WebsiteUrl;
                startupModel.LogoFileName = item.LogoFileName;
                startupModel.Logo = item.Logo;
                startupModel.EmployeeCount = item.EmployeeCount;
                startupModel.SectorId = item.SectorId;
                startupModel.SectorName = item.Sector.SectorName;
                startupModel.FoundingYear = item.FoundingYear;
                startupModel.CompanyLegalName = item.CompanyLegalName;
                startupModel.CompanyHeadquartersAddress = item.CompanyHeadquartersAddress;
                startupModel.CompanyContact = item.CompanyContact;
                startupModel.ServiceDescription = item.ServiceDescription;
                startupModel.BusinessModel = item.BusinessModel;
                startupModel.TargetCustomerBase = item.TargetCustomerBase;
                startupModel.TargetMarket = item.TargetMarket;
                startupModel.ManagementInfo = item.ManagementInfo;
                startupModel.IsStealth = item.IsStealth;
                startupModel.IsActive = item.IsActive;
                founderModel.StartupDeatailModel = startupModel;
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
                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }
        /// <summary>
        /// Send for verification founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SendForVerification(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {

            var message = string.Empty;
            var existingRecord = _startupContext.FounderVerificationDetails.Any(x => x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                var verifyEntity = new FounderVerificationDetail();
                verifyEntity.UserId = model.UserId;
                verifyEntity.SendForVerification = true;
                verifyEntity.Live = false;
                verifyEntity.Preview = false;
                verifyEntity.Verified = false;
                verifyEntity.CreatedDate = DateTime.Now;
                verifyEntity.CreatedBy = model.LoggedUserId;
                verifyEntity.IsActive = true;
                _startupContext.FounderVerificationDetails.Add(verifyEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.SentForVerification;
            }
            else
            {
                var verifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    verifyEntity.UserId = model.UserId;
                    verifyEntity.SendForVerification = true;
                    verifyEntity.Live = false;
                    verifyEntity.Preview = false;
                    verifyEntity.Verified = false;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.UserId;
                    verifyEntity.IsActive = true;
                    _startupContext.FounderVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.SentForVerification;


                }
            }
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Send for verification";
            userAuditLog.Description = "Send for verification";
            userAuditLog.UserId = model.UserId;
            userAuditLog.CreatedBy = model.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return message;
        }
        /// <summary>
        /// Approve/Notpprove founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string VerificationApprove(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var verification = new FounderVerificationDetail();
            var existingRecord = _startupContext.FounderVerificationDetails.Any(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var verifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
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
                        message = GlobalConstants.FounderNotApproved;
                        strBody.Append("<body>");
                        strBody.Append("<P>StartupX</P>");
                        strBody.Append("<h2>Your details are not verified because" + " " + model.Comment + "</h2>");
                        strBody.Append("</body>");
                        emailSenderModel.Subject = GlobalConstants.FounderNotApproved;

                        /// Notification Add
                        var notification = new NotificationModel();
                        notification.Message = "Your profile not verified because" + " " + model.Comment;
                        notification.UserId = model.UserId;
                        notification.LoggedUserId = model.LoggedUserId;
                        _notificationService.AddNotification(notification);
                    }
                    else
                    {
                        message = GlobalConstants.FounderApproved;
                        strBody.Append("<body>");
                        strBody.Append("<P>StartupX</P>");
                        strBody.Append("<h2>Your details are verified Successfully</h2>");
                        strBody.Append("</body>");
                        emailSenderModel.Subject = GlobalConstants.FounderApproved;

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
                    verifyEntity.IsActive = true;

                    emailSenderModel.ToAddress = userEnitity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }
                    _startupContext.FounderVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Approve/NotApprove verification";
                    userAuditLog.Description = "Verification Approved/NotApproved";
                    userAuditLog.UserId = model.UserId;
                    userAuditLog.CreatedBy = model.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);

                }
            }



            return message;
        }
        /// <summary>
        /// Make it live founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string LiveFounder(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.FounderVerificationDetails.Any(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var verifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    verifyEntity.UserId = model.UserId;
                    verifyEntity.Live = model.Live;
                    verifyEntity.Preview = model.Preview;
                    verifyEntity.SendForVerification = true;
                    verifyEntity.RaiseFunding = false;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.LoggedUserId;
                    verifyEntity.IsActive = true;

                    var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.UserId);
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("<P>StartupX</P>");
                    strBody.Append("<h2>Your company is set as a live now</h2>");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailModel();
                    emailSenderModel.ToAddress = userEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = GlobalConstants.FounderMakeAsLive;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }

                    _startupContext.FounderVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.FounderMakeAsLive;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Founder Live";
                    userAuditLog.Description = "Founder set as a live";
                    userAuditLog.UserId = model.UserId;
                    userAuditLog.CreatedBy = model.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);

                    /// Notification Add
                    var notification = new NotificationModel();
                    notification.Message = "Your company is set as a live now";
                    notification.UserId = model.UserId;
                    notification.LoggedUserId = model.LoggedUserId;
                    _notificationService.AddNotification(notification);
                }
            }


            return message;
        }
        /// <summary>
        /// Make it preview founder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string PreviewFounder(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.FounderVerificationDetails.Any(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
            if (!existingRecord)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var verifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.FounderVerifyId == model.FounderVerifyId && x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    verifyEntity.UserId = model.UserId;
                    verifyEntity.GaugingAmount = model.GaugingAmount;
                    verifyEntity.Live = model.Live;
                    verifyEntity.Preview = model.Preview;
                    verifyEntity.RaiseFunding = false;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.LoggedUserId;
                    verifyEntity.IsActive = true;

                    var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.UserId);
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("<P>StartupX</P>");
                    strBody.Append("<h2>Your company is set as a preview now</h2>");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailModel();
                    emailSenderModel.ToAddress = userEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = GlobalConstants.FounderMakeAsPreview;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }

                    _startupContext.FounderVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.FounderMakeAsPreview;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Founder Preview";
                    userAuditLog.Description = "Founder set as a preview";
                    userAuditLog.UserId = model.UserId;
                    userAuditLog.CreatedBy = model.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);

                    /// Notification Add
                    var notification = new NotificationModel();
                    notification.Message = "Your campany is set as a preview now";
                    notification.UserId = model.UserId;
                    notification.LoggedUserId = model.LoggedUserId;
                    _notificationService.AddNotification(notification);
                }
            }
            return message;
        }

        /// <summary>
        /// Founder Profile completion
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FounderVerificationModel FounderProfileCompletion(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var founderModel = new FounderVerificationModel();
            int startupDetailsFound = _startupContext.StartUpDetails.Where(x => x.UserId == userId && x.IsActive == true).Count();
            int startupDetailsIsStealthFound = _startupContext.StartUpDetails.Where(x => x.UserId == userId && x.IsStealth == false && x.IsActive == true).Count();
            int founderDetailsFound = _startupContext.FounderDetails.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Distinct().Count();
            int fundingDetailsFound = _startupContext.FundingDetails.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Distinct().Count();
            int founderDocumentFound = _startupContext.FounderInvestorDocuments.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Distinct().Count();
            founderModel.FounderProfileCompletion = (startupDetailsFound + startupDetailsIsStealthFound + founderDetailsFound + fundingDetailsFound + founderDocumentFound).ToString();
            founderModel.Verified = _startupContext.FounderVerificationDetails.Any(x => x.UserId == userId && x.Verified == true);
            founderModel.FounderDetailCount = _startupContext.FounderDetails.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Count();
            founderModel.FundingDetailCount = _startupContext.FundingDetails.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.UserId).Count();
            //founderModel.VerifiedCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == true && x.IsActive == true).Count();
            //founderModel.NonVerifiedCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == false && x.IsActive == true).Count();
            //founderModel.LiveCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == true && x.IsActive == true).Count();
            //founderModel.PreviewCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == false && x.Preview == true && x.IsActive == true).Count();
            //founderModel.StealthCount = _startupContext.StartUpDetails.Where(x => x.IsStealth == true && x.IsActive == true).Count();

            return founderModel;
        }
        /// <summary>
        /// Founder Status Count
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FounderVerificationModel FounderStatusCount(ref ErrorResponseModel errorResponseModel)
        {
            var founderModel = new FounderVerificationModel();
            founderModel.VerifiedCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == true && x.IsActive == true).Count();
            founderModel.NonVerifiedCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Verified == false && x.IsActive == true).Count();
            founderModel.LiveCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == true && x.IsActive == true).Count();
            founderModel.PreviewCount = _startupContext.FounderVerificationDetails.Where(x => x.SendForVerification == true && x.Live == false && x.Preview == true && x.IsActive == true).Count();
            founderModel.StealthCount = _startupContext.StartUpDetails.Where(x => x.IsStealth == true && x.IsActive == true).Count();
            founderModel.FounderRaiseCount = _startupContext.FounderVerificationDetails.Where(x => x.RaiseFunding == true && x.IsActive == true).Count();
            return founderModel;
        }

        /// <summary>
        /// Request Raise Funding
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string RequestRaiseFunding(FounderVerificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.FounderVerificationDetails.Any(x => x.UserId == model.UserId && x.IsActive == true);
            var userEnitity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.UserId);
            if (!existingRecord)
            {
                var verifyEntity = new FounderVerificationDetail();
                verifyEntity.UserId = model.UserId;
                verifyEntity.RaiseFunding = true;
                verifyEntity.CreatedDate = DateTime.Now;
                verifyEntity.CreatedBy = model.LoggedUserId;
                verifyEntity.IsActive = true;
                _startupContext.FounderVerificationDetails.Add(verifyEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RequestRaiseFundingMessage;
            }
            else
            {
                var verifyEntity = _startupContext.FounderVerificationDetails.FirstOrDefault(x => x.UserId == model.UserId && x.IsActive == true);
                if (verifyEntity != null)
                {
                    verifyEntity.UserId = model.UserId;
                    verifyEntity.RaiseFunding = true;
                    verifyEntity.UpdatedDate = DateTime.Now;
                    verifyEntity.UpdatedBy = model.UserId;
                    verifyEntity.IsActive = true;
                    _startupContext.FounderVerificationDetails.Update(verifyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RequestRaiseFundingMessage;
                }
            }
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<body>");
            strBody.Append("<P>StartupX</P>");
            strBody.Append("<h2>You have requested raise funding</h2>");
            strBody.Append("</body>");
            var emailSenderModel = new EmailModel();
            emailSenderModel.ToAddress = userEnitity.EmailId;
            emailSenderModel.Body = strBody.ToString();
            emailSenderModel.isHtml = true;
            emailSenderModel.Subject = GlobalConstants.RequestRaiseFundingMessage;
            emailSenderModel.sentStatus = true;
            if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
            {
                _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
            }
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = " Raise Funding";
            userAuditLog.Description = "Raise funding Request";
            userAuditLog.UserId = model.UserId;
            userAuditLog.CreatedBy = model.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);

            return message;
        }
        /// <summary>
        /// Get All Raise funding founder details
        /// </summary>
        /// <returns></returns>
        public List<FounderModelDetails> GetAllFounderRaiseDetails(ref ErrorResponseModel errorResponseModel)
        {
            List<FounderModelDetails> founderModelList = new List<FounderModelDetails>();
            var founderEntity = _startupContext.FounderVerificationDetails.Where(x => x.RaiseFunding == true && x.IsActive == true).ToList();
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
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
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
                founderModelList.Add(founderModel);
            }
            return founderModelList;
        }

    }
}
