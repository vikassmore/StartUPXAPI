using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class UserMaster
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? UserName { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public int FounderTypeId { get; set; }

    public int RoleId { get; set; }

    public bool? ServiceStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BankDetail> BankDetails { get; } = new List<BankDetail>();

    public virtual ICollection<FounderDetail> FounderDetails { get; } = new List<FounderDetail>();

    public virtual ICollection<FounderInvestorDocument> FounderInvestorDocuments { get; } = new List<FounderInvestorDocument>();

    public virtual FounderType FounderType { get; set; } = null!;

    public virtual ICollection<FounderVerificationDetail> FounderVerificationDetails { get; } = new List<FounderVerificationDetail>();

    public virtual ICollection<FundingDetail> FundingDetails { get; } = new List<FundingDetail>();

    public virtual ICollection<InvestmentDetail> InvestmentDetails { get; } = new List<InvestmentDetail>();

    public virtual ICollection<InvestmentOpportunityDetail> InvestmentOpportunityDetails { get; } = new List<InvestmentOpportunityDetail>();

    public virtual ICollection<InvestorDetail> InvestorDetails { get; } = new List<InvestorDetail>();

    public virtual ICollection<InvestorInvestmentDetail> InvestorInvestmentDetails { get; } = new List<InvestorInvestmentDetail>();

    public virtual ICollection<InvestorVerificationDetail> InvestorVerificationDetails { get; } = new List<InvestorVerificationDetail>();

    public virtual ICollection<Otpautherization> Otpautherizations { get; } = new List<Otpautherization>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<ServiceDetail> ServiceDetails { get; } = new List<ServiceDetail>();

    public virtual ICollection<ServiceLeadManagement> ServiceLeadManagements { get; } = new List<ServiceLeadManagement>();

    public virtual ICollection<SocialMediaLogin> SocialMediaLogins { get; } = new List<SocialMediaLogin>();

    public virtual ICollection<StartUpDetail> StartUpDetails { get; } = new List<StartUpDetail>();

    public virtual ICollection<SuitabilityQuestion> SuitabilityQuestions { get; } = new List<SuitabilityQuestion>();

    public virtual ICollection<TrustedContactPerson> TrustedContactPeople { get; } = new List<TrustedContactPerson>();

    public virtual ICollection<UserAuditLog> UserAuditLogs { get; } = new List<UserAuditLog>();
}
