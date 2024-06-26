using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class UserAuditLog
{
    public long AuditLogId { get; set; }

    public string? Action { get; set; }

    public string? Description { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual UserMaster? User { get; set; }
}
