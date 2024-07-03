using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class InvestorInvestmentModel
    {
        public int InvestorInvestmentId { get; set; }

        public string? InvestmentAmount { get; set; }

        public string? IndicateInterest { get; set; }
        public string? InvestmentRound { get; set; }

        public int FounderVerifyId { get; set; }

        public int InvestorUserId { get; set; }

        public bool? OnWatchList { get; set; }

        public bool? RequestOffering { get; set; }

        public bool IsActive { get; set; }
        public int LoggedUserId { get; set; }
       
    }
    public class InvestorInvestmentList
    {
        public int InvestorInvestmentId { get; set; }

        public string? InvestmentAmount { get; set; }

        public string? InvestmentRound { get; set; }
        public string? IndicateInterest { get; set; }

        public int FounderVerifyId { get; set; }

        public int InvestorUserId { get; set; }

        public string? InvestorName { get; set; }
        public bool? OnWatchList { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CretedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpadteDate { get; set; }

        public int? UpadateBy { get; set; }

        public FounderModelDetails founderModelDetails { get; set; }
     
    }
}
