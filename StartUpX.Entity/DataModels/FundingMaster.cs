using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class FundingMaster
{
    public int FundingId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<FundingDetail> FundingDetails { get; } = new List<FundingDetail>();
}
