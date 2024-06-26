using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class StartUpDetail
{
    public int StartupId { get; set; }

    public string? StartUpName { get; set; }

    public string? Address { get; set; }

    public string? CompanyLegalName { get; set; }

    public string? CompanyEmailId { get; set; }

    public string? CompanyContact { get; set; }

    public string? CompanyHeadquartersAddress { get; set; }

    public string? FoundingYear { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? LogoFileName { get; set; }

    public byte[]? Logo { get; set; }

    public string? EmployeeCount { get; set; }

    public string? CompanyDescription { get; set; }

    public string? ServiceDescription { get; set; }

    public string? BusinessModel { get; set; }

    public string? TargetCustomerBase { get; set; }

    public string? TargetMarket { get; set; }

    public string? ManagementInfo { get; set; }

    public bool IsStealth { get; set; }

    public int SectorId { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual SectorDetail Sector { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;
}
