using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestmentDetail
{
    public int InvestmentId { get; set; }

    public string? InvestmentStage { get; set; }

    public string? InvestmentSector { get; set; }

    public string? InvestmentAmount { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
