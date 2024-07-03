using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class InvestorVerificationModel
    {
        public int InvestorVerifyId { get; set; }

        public int UserId { get; set; }

        public bool? SendForVerification { get; set; }

        public bool? Verified { get; set; }

        public string? Comment { get; set; }
        public bool IsActive { get; set; }
        public string? InvestorProfileCompletion { get; set; }
        public int? VerifiedCount { get; set; }
        public int? NonVerifiedCount { get; set; }
        public int? RequestFundingCount { get; set; }
        public int? InvestorsInvestedCount { get; set; }
        public int? IndicateInvestmentCount { get; set; }
        public int LoggedUserId { get; set; }

    }
    public class InvestorModel
    {
        public int InvestorVerifyId { get; set; }

        public int UserId { get; set; }

        public bool? SendForVerification { get; set; }

        public bool? Verified { get; set; }

        public string? Comment { get; set; }
        public bool IsActive { get; set; }
        //public InvestorDetailModel investordetailModel { get; set; }
        public InvestorDetailModelList InvestorDetailModel { get; set; }

        public InvestmentDetailModel investmentDeatail { get; set; }

        public List<FounderInvestorDocumentModel> founderInvestorDocumentList { get; set; }

    }

    public class RequestOfferModel
    {
        public string? InvestorName { get; set; }

        public int? UserId { get; set; }
        public bool? Verified { get; set; }
        public int? InvestorVerifiedId { get; set; }
        public int? FounderVerifiedId { get; set; }
        public int? investorId { get; set; }
        public StartupDeatailModelList StartupDeatailModel { get; set; }
    }

    public class InvestorsInvestmentsModel
    {
        public string? InvestorName { get; set; }

        public int? UserId { get; set; }
        public bool? Verified { get; set; }
        public int? InvestorVerifiedId { get; set; }
        public int? FounderVerifiedId { get; set; }
        public int? investorId { get; set; }
        public StartupDeatailModelList StartupDeatailModel { get; set; }
    }

    public class IndicateInterestModel
    {
        public string? InvestorName { get; set; }

        public int? UserId { get; set; }
        public bool? Verified { get; set; }
        public int? InvestorVerifiedId { get; set; }
        public int? FounderVerifiedId { get; set; }
        public int? investorId { get; set; }
        public string? IndicateInterest { get; set; }
        public StartupDeatailModelList StartupDeatailModel { get; set; }
    }
}
