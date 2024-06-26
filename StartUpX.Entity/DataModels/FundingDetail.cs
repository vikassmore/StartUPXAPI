using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class FundingDetail
{
    public int FundingDetailsId { get; set; }

    public string? ShareClass { get; set; }

    public string? DateFinancing { get; set; }

    public string? SharesOutstanding { get; set; }

    public string? IssuePrice { get; set; }

    public string? ConversionPrice { get; set; }

    public string? TotalFinancingSize { get; set; }

    public string? LiquidityRank { get; set; }

    public string? LiquidationPreference { get; set; }

    public string? DividendRate { get; set; }

    public string? DividendType { get; set; }

    public string? VotesPerShare { get; set; }

    public string? RedemptionRights { get; set; }

    public string? ConvertibleToOnPublicListing { get; set; }

    public string? ParticipatingPreferred { get; set; }

    public string? QualifiedIpo { get; set; }

    public string? OtherKeyProvisions { get; set; }

    public int FundingId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual FundingMaster Funding { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;
}
