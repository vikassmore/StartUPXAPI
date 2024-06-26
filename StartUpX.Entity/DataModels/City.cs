using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public int StateId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InvestorDetail> InvestorDetails { get; } = new List<InvestorDetail>();

    public virtual ICollection<StartUpDetail> StartUpDetails { get; } = new List<StartUpDetail>();

    public virtual State State { get; set; } = null!;

    public virtual ICollection<TrustedContactPerson> TrustedContactPeople { get; } = new List<TrustedContactPerson>();
}
