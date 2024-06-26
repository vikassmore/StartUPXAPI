using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class SuitabilityQuestion
{
    public int SquestionId { get; set; }

    public string? MaximumInvestent { get; set; }

    public string? AcceptInvestment { get; set; }

    public string? ReleventInvestment { get; set; }

    public string? LiquidWorth { get; set; }

    public string? RiskFector { get; set; }

    public string? ConfiditialAgreement { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
