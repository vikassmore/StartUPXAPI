using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class AccreditedInvestorMaster
{
    public int AccreditedInvestorId { get; set; }

    public string? AccreditedInvestorName { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }
}
