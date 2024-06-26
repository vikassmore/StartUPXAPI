using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class FounderVerificationDetail
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

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InvestorInvestmentDetail> InvestorInvestmentDetails { get; } = new List<InvestorInvestmentDetail>();

    public virtual UserMaster User { get; set; } = null!;
}
