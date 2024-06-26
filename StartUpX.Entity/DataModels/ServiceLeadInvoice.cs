using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class ServiceLeadInvoice
{
    public int ServiceLeadInvoiceId { get; set; }

    public string? InvoiceFileName { get; set; }

    public string? InvoiceFilePath { get; set; }

    public int ServiceCaseId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ServiceLeadManagement ServiceCase { get; set; } = null!;
}
