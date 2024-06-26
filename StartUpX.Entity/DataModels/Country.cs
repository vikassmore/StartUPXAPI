using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? CountryCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InvestorDetail> InvestorDetails { get; } = new List<InvestorDetail>();

    public virtual ICollection<StartUpDetail> StartUpDetails { get; } = new List<StartUpDetail>();

    public virtual ICollection<State> States { get; } = new List<State>();

    public virtual ICollection<TrustedContactPerson> TrustedContactPeople { get; } = new List<TrustedContactPerson>();
}
