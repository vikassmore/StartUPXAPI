using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class State
{
    public int StateId { get; set; }

    public string? StateName { get; set; }

    public string? StateCode { get; set; }

    public int CountryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<InvestorDetail> InvestorDetails { get; } = new List<InvestorDetail>();

    public virtual ICollection<StartUpDetail> StartUpDetails { get; } = new List<StartUpDetail>();

    public virtual ICollection<TrustedContactPerson> TrustedContactPeople { get; } = new List<TrustedContactPerson>();
}
