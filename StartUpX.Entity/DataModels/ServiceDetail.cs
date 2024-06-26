using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class ServiceDetail
{
    public int ServiceId { get; set; }

    public string? ServiceProviderName { get; set; }

    public string? Category { get; set; }

    public string? ContactInformation { get; set; }

    public string? TagsKeywords { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ServiceLeadManagement> ServiceLeadManagements { get; } = new List<ServiceLeadManagement>();

    public virtual ICollection<ServicePortfolio> ServicePortfolios { get; } = new List<ServicePortfolio>();

    public virtual UserMaster? User { get; set; }
}
