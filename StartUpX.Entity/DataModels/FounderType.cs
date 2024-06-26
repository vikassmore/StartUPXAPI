using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class FounderType
{
    public int FounderTypeId { get; set; }

    public string? FounderName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InvestorDetail> InvestorDetails { get; } = new List<InvestorDetail>();

    public virtual ICollection<UserMaster> UserMasters { get; } = new List<UserMaster>();
}
