using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class ServicePortfolio
{
    public int ServicePortFolioId { get; set; }

    public string? DocumentName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public int ServiceId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ServiceDetail Service { get; set; } = null!;
}
