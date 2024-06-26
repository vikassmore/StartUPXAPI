using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class ServiceLeadManagement
{
    public int ServiceCaseId { get; set; }

    public int ServiceId { get; set; }

    public DateTime? ServiceInterestDate { get; set; }

    public string? IntrestedServiceNames { get; set; }

    public string? Status { get; set; }

    public string? Comment { get; set; }

    public DateTime? AcceptedDate { get; set; }

    public int FounderUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster FounderUser { get; set; } = null!;

    public virtual ServiceDetail Service { get; set; } = null!;

    public virtual ICollection<ServiceLeadInvoice> ServiceLeadInvoices { get; } = new List<ServiceLeadInvoice>();
}
