using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestementCategory
{
    public int InvCategoryId { get; set; }

    public string? InvCategoryName { get; set; }

    public string? InvCategoryDescription { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }
}
