using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class SocialMediaLogin
{
    public int SocialId { get; set; }

    public int UserId { get; set; }

    public string? Provider { get; set; }

    public string? ProviderId { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
