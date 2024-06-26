using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StartUpX.Entity.DataModels;

public partial class StartUpDBContext : DbContext
{
    public StartUpDBContext()
    {
    }

    public StartUpDBContext(DbContextOptions<StartUpDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<EmployeeCountMaster> EmployeeCountMasters { get; set; }

    public virtual DbSet<Faqmaster> Faqmasters { get; set; }

    public virtual DbSet<FounderDetail> FounderDetails { get; set; }

    public virtual DbSet<FounderInvestorDocument> FounderInvestorDocuments { get; set; }

    public virtual DbSet<FounderType> FounderTypes { get; set; }

    public virtual DbSet<FounderVerificationDetail> FounderVerificationDetails { get; set; }

    public virtual DbSet<FundingDetail> FundingDetails { get; set; }

    public virtual DbSet<FundingMaster> FundingMasters { get; set; }

    public virtual DbSet<InvestmentDetail> InvestmentDetails { get; set; }

    public virtual DbSet<InvestmentOpportunityDetail> InvestmentOpportunityDetails { get; set; }

    public virtual DbSet<InvestorDetail> InvestorDetails { get; set; }

    public virtual DbSet<InvestorInvestmentDetail> InvestorInvestmentDetails { get; set; }

    public virtual DbSet<InvestorVerificationDetail> InvestorVerificationDetails { get; set; }

    public virtual DbSet<NotableInvestorMaster> NotableInvestorMasters { get; set; }

    public virtual DbSet<NotificationDetail> NotificationDetails { get; set; }

    public virtual DbSet<Otpautherization> Otpautherizations { get; set; }

    public virtual DbSet<PolicyMaster> PolicyMasters { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SectorDetail> SectorDetails { get; set; }

    public virtual DbSet<ServiceDetail> ServiceDetails { get; set; }

    public virtual DbSet<ServiceLeadInvoice> ServiceLeadInvoices { get; set; }

    public virtual DbSet<ServiceLeadManagement> ServiceLeadManagements { get; set; }

    public virtual DbSet<ServicePortfolio> ServicePortfolios { get; set; }

    public virtual DbSet<SocialMediaLogin> SocialMediaLogins { get; set; }

    public virtual DbSet<StartUpDetail> StartUpDetails { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SuitabilityQuestion> SuitabilityQuestions { get; set; }

    public virtual DbSet<TrustedContactPerson> TrustedContactPeople { get; set; }

    public virtual DbSet<UserAuditLog> UserAuditLogs { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=meshbasqldb.c5nzv8jpq3sq.ap-south-1.rds.amazonaws.com;Initial Catalog=StartUpX;User ID=StartUpXAdmin;Password=Admin1234;Trusted_Connection=True;encrypt=false;TrustServerCertificate=True;Integrated Security =false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasKey(e => e.BankId);

            entity.ToTable("BankDetail");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.BranchName).HasMaxLength(100);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(100)
                .HasColumnName("IFSCCode");

            entity.HasOne(d => d.User).WithMany(p => p.BankDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankDetail_UserMaster");
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_InvestementCategory");

            entity.ToTable("CategoryMaster");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_CityMaster");

            entity.ToTable("City");

            entity.Property(e => e.CityName).HasMaxLength(100);

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_State");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK_CountryMaster");

            entity.ToTable("Country");

            entity.Property(e => e.CountryCode).HasMaxLength(50);
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<EmployeeCountMaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeCountId);

            entity.ToTable("EmployeeCountMaster");

            entity.Property(e => e.EmployeeCount).HasMaxLength(100);
        });

        modelBuilder.Entity<Faqmaster>(entity =>
        {
            entity.HasKey(e => e.FrequentlyAqid);

            entity.ToTable("FAQMaster");

            entity.Property(e => e.FrequentlyAqid).HasColumnName("FrequentlyAQId");
            entity.Property(e => e.Answer).HasMaxLength(200);
            entity.Property(e => e.Question).HasMaxLength(200);
        });

        modelBuilder.Entity<FounderDetail>(entity =>
        {
            entity.HasKey(e => e.FounderId).HasName("PK_Founder_Details");

            entity.ToTable("FounderDetail");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(15);

            entity.HasOne(d => d.User).WithMany(p => p.FounderDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FounderDetail_UserMaster");
        });

        modelBuilder.Entity<FounderInvestorDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK_DocumentDetail");

            entity.ToTable("FounderInvestorDocument");

            entity.Property(e => e.DocumentName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.FounderInvestorDocuments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentDetail_UserMaster");
        });

        modelBuilder.Entity<FounderType>(entity =>
        {
            entity.ToTable("FounderType");

            entity.Property(e => e.FounderName).HasMaxLength(100);
        });

        modelBuilder.Entity<FounderVerificationDetail>(entity =>
        {
            entity.HasKey(e => e.FounderVerifyId);

            entity.ToTable("FounderVerificationDetail");

            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.GaugingAmount).HasMaxLength(100);
            entity.Property(e => e.RaiseFunding).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.User).WithMany(p => p.FounderVerificationDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FounderVerificationDetail_UserMaster");
        });

        modelBuilder.Entity<FundingDetail>(entity =>
        {
            entity.HasKey(e => e.FundingDetailsId).HasName("PK_FundingDetails");

            entity.ToTable("FundingDetail");

            entity.Property(e => e.ConversionPrice).HasMaxLength(100);
            entity.Property(e => e.ConvertibleToOnPublicListing).HasMaxLength(100);
            entity.Property(e => e.DateFinancing).HasMaxLength(50);
            entity.Property(e => e.DividendRate).HasMaxLength(100);
            entity.Property(e => e.DividendType).HasMaxLength(100);
            entity.Property(e => e.IssuePrice).HasMaxLength(100);
            entity.Property(e => e.LiquidationPreference).HasMaxLength(100);
            entity.Property(e => e.LiquidityRank).HasMaxLength(100);
            entity.Property(e => e.OtherKeyProvisions).HasMaxLength(200);
            entity.Property(e => e.ParticipatingPreferred).HasMaxLength(100);
            entity.Property(e => e.QualifiedIpo)
                .HasMaxLength(200)
                .HasColumnName("QualifiedIPO");
            entity.Property(e => e.RedemptionRights).HasMaxLength(100);
            entity.Property(e => e.ShareClass).HasMaxLength(100);
            entity.Property(e => e.SharesOutstanding).HasMaxLength(100);
            entity.Property(e => e.TotalFinancingSize).HasMaxLength(100);
            entity.Property(e => e.VotesPerShare).HasMaxLength(100);

            entity.HasOne(d => d.Funding).WithMany(p => p.FundingDetails)
                .HasForeignKey(d => d.FundingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FundingDetails_FundingMaster");

            entity.HasOne(d => d.User).WithMany(p => p.FundingDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FundingDetail_UserMaster");
        });

        modelBuilder.Entity<FundingMaster>(entity =>
        {
            entity.HasKey(e => e.FundingId);

            entity.ToTable("FundingMaster");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<InvestmentDetail>(entity =>
        {
            entity.HasKey(e => e.InvestmentId);

            entity.ToTable("InvestmentDetail");

            entity.Property(e => e.InvestmentAmount).HasMaxLength(100);
            entity.Property(e => e.InvestmentSector).HasMaxLength(100);
            entity.Property(e => e.InvestmentStage).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.InvestmentDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestmentDetail_UserMaster");
        });

        modelBuilder.Entity<InvestmentOpportunityDetail>(entity =>
        {
            entity.HasKey(e => e.InvestmentOpportunityId);

            entity.ToTable("InvestmentOpportunityDetail");

            entity.Property(e => e.Discount).HasMaxLength(50);
            entity.Property(e => e.ExpectedSharePrice).HasMaxLength(100);
            entity.Property(e => e.FundName).HasMaxLength(100);
            entity.Property(e => e.FundStrategy).HasMaxLength(100);
            entity.Property(e => e.ImpliedCompanyValuation).HasMaxLength(100);
            entity.Property(e => e.LatestPostMoneyValuation).HasMaxLength(100);
            entity.Property(e => e.MinimumInvestmentSize).HasMaxLength(100);
            entity.Property(e => e.SalesFee).HasMaxLength(50);
            entity.Property(e => e.SecurityType).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.InvestmentOpportunityDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestmentOpportunityDetail_UserMaster");
        });

        modelBuilder.Entity<InvestorDetail>(entity =>
        {
            entity.HasKey(e => e.InvestorId);

            entity.ToTable("InvestorDetail");

            entity.Property(e => e.Address1).HasMaxLength(200);
            entity.Property(e => e.Address2).HasMaxLength(200);
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.ProfileType).HasMaxLength(100);

            entity.HasOne(d => d.City).WithMany(p => p.InvestorDetails)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorDetail_City");

            entity.HasOne(d => d.Country).WithMany(p => p.InvestorDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorDetail_Country");

            entity.HasOne(d => d.FounderType).WithMany(p => p.InvestorDetails)
                .HasForeignKey(d => d.FounderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorDetail_FounderType");

            entity.HasOne(d => d.State).WithMany(p => p.InvestorDetails)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorDetail_State");

            entity.HasOne(d => d.User).WithMany(p => p.InvestorDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorDetail_UserMaster");
        });

        modelBuilder.Entity<InvestorInvestmentDetail>(entity =>
        {
            entity.HasKey(e => e.InvestorInvestmentId);

            entity.ToTable("InvestorInvestmentDetail");

            entity.Property(e => e.IndicateInterest).HasMaxLength(50);
            entity.Property(e => e.InvestmentAmount).HasMaxLength(50);
            entity.Property(e => e.InvestmentRound).HasMaxLength(50);
            entity.Property(e => e.RequestOffering).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.FounderVerify).WithMany(p => p.InvestorInvestmentDetails)
                .HasForeignKey(d => d.FounderVerifyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorInvestmentDetail_FounderVerificationDetail");

            entity.HasOne(d => d.InvestorUser).WithMany(p => p.InvestorInvestmentDetails)
                .HasForeignKey(d => d.InvestorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorInvestmentDetail_UserMaster");
        });

        modelBuilder.Entity<InvestorVerificationDetail>(entity =>
        {
            entity.HasKey(e => e.InvestorVerifyId);

            entity.ToTable("InvestorVerificationDetail");

            entity.Property(e => e.Comment).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.InvestorVerificationDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorVerificationDetail_UserMaster");
        });

        modelBuilder.Entity<NotableInvestorMaster>(entity =>
        {
            entity.HasKey(e => e.NotableInvestorId);

            entity.ToTable("NotableInvestorMaster");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(15);
        });

        modelBuilder.Entity<NotificationDetail>(entity =>
        {
            entity.HasKey(e => e.NotificationId);

            entity.ToTable("NotificationDetail");

            entity.Property(e => e.Message).HasMaxLength(200);
        });

        modelBuilder.Entity<Otpautherization>(entity =>
        {
            entity.HasKey(e => e.Otpid).HasName("PK_OPTAutherization");

            entity.ToTable("OTPAutherization");

            entity.Property(e => e.Otpid).HasColumnName("OTPId");
            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .HasColumnName("OTP");
            entity.Property(e => e.OtpvalidDateTime).HasColumnName("OTPValidDateTime");

            entity.HasOne(d => d.User).WithMany(p => p.Otpautherizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTPAutherization_UserMaster");
        });

        modelBuilder.Entity<PolicyMaster>(entity =>
        {
            entity.HasKey(e => e.PolicyId);

            entity.ToTable("PolicyMaster");

            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_RoleMaster");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SectorDetail>(entity =>
        {
            entity.HasKey(e => e.SectorId).HasName("PK_SectorMaster");

            entity.ToTable("SectorDetail");

            entity.Property(e => e.SectorDescription).HasMaxLength(200);
            entity.Property(e => e.SectorName).HasMaxLength(100);
            entity.Property(e => e.SubSectorName).HasMaxLength(100);
        });

        modelBuilder.Entity<ServiceDetail>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK_ServiceMaster");

            entity.ToTable("ServiceDetail");

            entity.Property(e => e.Category).HasMaxLength(200);
            entity.Property(e => e.ContactInformation).HasMaxLength(200);
            entity.Property(e => e.ServiceProviderName).HasMaxLength(100);
            entity.Property(e => e.TagsKeywords).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.ServiceDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ServiceDetail_UserMaster");
        });

        modelBuilder.Entity<ServiceLeadInvoice>(entity =>
        {
            entity.ToTable("ServiceLeadInvoice");

            entity.HasOne(d => d.ServiceCase).WithMany(p => p.ServiceLeadInvoices)
                .HasForeignKey(d => d.ServiceCaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceLeadInvoice_ServiceLeadManagement");
        });

        modelBuilder.Entity<ServiceLeadManagement>(entity =>
        {
            entity.HasKey(e => e.ServiceCaseId).HasName("PK_ServiceCaseManagement");

            entity.ToTable("ServiceLeadManagement");

            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.IntrestedServiceNames).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.FounderUser).WithMany(p => p.ServiceLeadManagements)
                .HasForeignKey(d => d.FounderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceLeadManagement_UserMaster");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceLeadManagements)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceLeadManagement_ServiceDetail");
        });

        modelBuilder.Entity<ServicePortfolio>(entity =>
        {
            entity.ToTable("ServicePortfolio");

            entity.Property(e => e.DocumentName).HasMaxLength(100);

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePortfolios)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServicePortfolio_ServiceDetail");
        });

        modelBuilder.Entity<SocialMediaLogin>(entity =>
        {
            entity.HasKey(e => e.SocialId);

            entity.ToTable("SocialMediaLogin");

            entity.Property(e => e.AccessToken).HasMaxLength(200);
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.ProviderId).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.SocialMediaLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocialMediaLogin_UserMaster");
        });

        modelBuilder.Entity<StartUpDetail>(entity =>
        {
            entity.HasKey(e => e.StartupId).HasName("PK_StartUpDetails");

            entity.ToTable("StartUpDetail");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BusinessModel).HasMaxLength(500);
            entity.Property(e => e.CompanyContact).HasMaxLength(15);
            entity.Property(e => e.CompanyDescription).HasMaxLength(500);
            entity.Property(e => e.CompanyEmailId).HasMaxLength(100);
            entity.Property(e => e.CompanyHeadquartersAddress).HasMaxLength(200);
            entity.Property(e => e.CompanyLegalName).HasMaxLength(100);
            entity.Property(e => e.EmployeeCount).HasMaxLength(100);
            entity.Property(e => e.FoundingYear).HasMaxLength(10);
            entity.Property(e => e.ServiceDescription).HasMaxLength(500);
            entity.Property(e => e.StartUpName).HasMaxLength(100);
            entity.Property(e => e.TargetCustomerBase).HasMaxLength(500);
            entity.Property(e => e.TargetMarket).HasMaxLength(500);
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(100)
                .HasColumnName("WebsiteURL");

            entity.HasOne(d => d.City).WithMany(p => p.StartUpDetails)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StartUpDetails_City");

            entity.HasOne(d => d.Country).WithMany(p => p.StartUpDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StartUpDetails_Country");

            entity.HasOne(d => d.Sector).WithMany(p => p.StartUpDetails)
                .HasForeignKey(d => d.SectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StartUpDetail_SectorDetail");

            entity.HasOne(d => d.State).WithMany(p => p.StartUpDetails)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StartUpDetails_State");

            entity.HasOne(d => d.User).WithMany(p => p.StartUpDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StartUpDetail_UserMaster");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK_StateMaster");

            entity.ToTable("State");

            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.StateName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State_Country");
        });

        modelBuilder.Entity<SuitabilityQuestion>(entity =>
        {
            entity.HasKey(e => e.SquestionId);

            entity.ToTable("SuitabilityQuestion");

            entity.Property(e => e.AcceptInvestment).HasMaxLength(100);
            entity.Property(e => e.ConfiditialAgreement).HasMaxLength(100);
            entity.Property(e => e.LiquidWorth).HasMaxLength(100);
            entity.Property(e => e.MaximumInvestent).HasMaxLength(100);
            entity.Property(e => e.ReleventInvestment).HasMaxLength(100);
            entity.Property(e => e.RiskFector).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.SuitabilityQuestions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuitabilityQuestion_UserMaster");
        });

        modelBuilder.Entity<TrustedContactPerson>(entity =>
        {
            entity.HasKey(e => e.TrustedContactId).HasName("PK_TrustedContectPerson");

            entity.ToTable("TrustedContactPerson");

            entity.Property(e => e.Address1).HasMaxLength(200);
            entity.Property(e => e.Address2).HasMaxLength(200);
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsTrustedContact).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(10);
            entity.Property(e => e.ZipCode).HasMaxLength(10);

            entity.HasOne(d => d.City).WithMany(p => p.TrustedContactPeople)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrustedContactPerson_City");

            entity.HasOne(d => d.Country).WithMany(p => p.TrustedContactPeople)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrustedContactPerson_Country");

            entity.HasOne(d => d.State).WithMany(p => p.TrustedContactPeople)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrustedContactPerson_State");

            entity.HasOne(d => d.User).WithMany(p => p.TrustedContactPeople)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrustedContactPerson_UserMaster");
        });

        modelBuilder.Entity<UserAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId);

            entity.ToTable("UserAuditLog");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.UserAuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAuditLog_UserMaster");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("UserMaster");

            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.FounderType).WithMany(p => p.UserMasters)
                .HasForeignKey(d => d.FounderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMaster_FounderType");

            entity.HasOne(d => d.Role).WithMany(p => p.UserMasters)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMaster_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

