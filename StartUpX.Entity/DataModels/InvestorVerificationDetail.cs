using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestorVerificationDetail
{
    public int InvestorVerifyId { get; set; }

    public int UserId { get; set; }

    public bool? SendForVerification { get; set; }

    public bool? Verified { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
