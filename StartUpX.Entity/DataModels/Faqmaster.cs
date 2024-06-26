using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class Faqmaster
{
    public int FrequentlyAqid { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }
}
