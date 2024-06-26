using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestorInvestmentDetail
{
    public int InvestorInvestmentId { get; set; }

    public string? InvestmentAmount { get; set; }

    public string? InvestmentRound { get; set; }

    public string? IndicateInterest { get; set; }

    public int FounderVerifyId { get; set; }

    public int InvestorUserId { get; set; }

    public bool? OnWatchList { get; set; }

    public bool? RequestOffering { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual FounderVerificationDetail FounderVerify { get; set; } = null!;

    public virtual UserMaster InvestorUser { get; set; } = null!;
}
