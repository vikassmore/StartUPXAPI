using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestmentOpportunityDetail
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

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
