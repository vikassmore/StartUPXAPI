using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class NotificationDetail
{
    public int NotificationId { get; set; }

    public string? Message { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }
}
