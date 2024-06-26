using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FounderVerificationModel
    {
        public int FounderVerifyId { get; set; }

        public int UserId { get; set; }

        public bool? SendForVerification { get; set; }

        public bool? Verified { get; set; }

        public bool? Live { get; set; }

        public string? GaugingAmount { get; set; }

        public bool? Preview { get; set; }

        public string? Comment { get; set; }

        public bool? RaiseFunding { get; set; }

        public bool IsActive { get; set; }

        public string? FounderProfileCompletion { get; set; }
        public int? VerifiedCount { get; set; }
        public int? NonVerifiedCount { get; set; }
        public int? LiveCount { get; set; }
        public int? PreviewCount { get; set; }
        public int? StealthCount { get; set; }
        public int LoggedUserId { get; set; }
        public int FundingDetailCount { get; set; }
        public int FounderDetailCount { get; set; }
        public int FounderRaiseCount { get; set; }
    }
    public class FounderModelDetails
    {
        public int FounderVerifyId { get; set; }

        public int UserId { get; set; }

        public bool? SendForVerification { get; set; }

        public bool? Verified { get; set; }

        public bool? Live { get; set; }

        public string? GaugingAmount { get; set; }
        public int? GaugingPercentage { get; set; }

        public string? LastRoundPrice { get; set; }
        public double? LastValuation { get; set; }

        public double? SecondLastValuation { get; set; }

        public bool? Preview { get; set; }

        public string? Comment { get; set; }

        public bool? RaiseFunding { get; set; }

        public bool NewFounder { get; set; }

        public bool IsActive { get; set; }

        public string? NotableInvestorName { get; set; }

        public string? StartupName { get; set; }
        public StartupDeatailModelList StartupDeatailModel { get; set; }
        public List<FounderDeatailModel> FounderDeatail { get; set; }
        public List<FundingDetailsModel> FundingDetails { get; set;}
        public FundingDetailsModel FundingModelDetails { get; set; }
        public List<InvestorInvestmentList> InvestorInvestmentList { get; set; }
        public InvestmnetopportunityModel Investmnetopportunity { get; set; }
        public List<InvestmnetopportunityModel> InvestmnetopportunityList { get; set; }
        public List<FounderInvestorDocumentModel> founderInvestorDocumentList { get; set; }
        public List<CompitetorFounders> compitetorFounders { get; set; }

    }
    public class CompitetorFounders
    {
        public string? LastRoundPrice { get; set; }
        public double? LastValuation { get; set; }
        public string? GaugingAmount { get; set; }
        public int? GaugingPercentage { get; set; }
        public StartupDeatailModelList StartupDeatailModel { get; set; }
        public List<FounderDeatailModel> FounderDeatail { get; set; }
        public List<FundingDetailsModel> FundingDetails { get; set; }
    }
}
