using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class SectorDetail
{
    public int SectorId { get; set; }

    public string? SectorName { get; set; }

    public string? SectorDescription { get; set; }

    public string? SubSectorName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<StartUpDetail> StartUpDetails { get; } = new List<StartUpDetail>();
}
