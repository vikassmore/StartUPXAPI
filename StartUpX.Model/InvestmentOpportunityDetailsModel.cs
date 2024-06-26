using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class InvestmentOpportunityDetailsModel
    {
        public int InvestmentOpportunityId { get; set; }

        public string? FundName { get; set; }

        public string? FundStrategy { get; set; }

        public string? SalesFee { get; set; }

        public string? ExpectedSharePrice { get; set; }

        public string? SecurityType { get; set; }

        public string? ImpliedCompanyValuation { get; set; }

        public string? LatestPostMoneyValuation { get; set; }

        public string? Discount { get; set; }

        public string? MinimumInvestmentSize { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }
    }
    public class InvestmnetopportunityModel
    {
        public int InvestmentOpportunityId { get; set; }

        public string? FundName { get; set; }

        public string? FundStrategy { get; set; }

        public string? SalesFee { get; set; }

        public string? ExpectedSharePrice { get; set; }

        public string? SecurityType { get; set; }

        public string? ImpliedCompanyValuation { get; set; }

        public string? LatestPostMoneyValuation { get; set; }

        public string? Discount { get; set; }

        public string? MinimumInvestmentSize { get; set; }

        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int? LoggedUserId { get; set; }

    }
}
