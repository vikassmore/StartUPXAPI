using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class InvestorDetail
{
    public int InvestorId { get; set; }

    public string? ProfileType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public int FounderTypeId { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public int? ZipCode { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual FounderType FounderType { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;
}
