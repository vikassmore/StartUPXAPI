using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class StartupDeatailModel
    {
        public int StartupId { get; set; }

        public string? StartUpName { get; set; }        

        public string? Address { get; set; }

        public string? CompanyLegalName { get; set; }

        public string? CompanyContact { get; set; }

        public string? CompanyEmailId { get; set; }

        public string? CompanyHeadquartersAddress { get; set; }

        public string? FoundingYear { get; set; }


        public string? WebsiteUrl { get; set; }

       // public byte[]? Logo { get; set; }


        public string? EmployeeCount { get; set; }

        public string? CompanyDescription { get; set; }

        public string? ServiceDescription { get; set; }

        public string? BusinessModel { get; set; }

        public string? TargetCustomerBase { get; set; }

        public string? TargetMarket { get; set; }

        public string? ManagementInfo { get; set; }

        public bool IsStealth { get; set; }

        public int SectorId { get; set; }
        public string? SectorName { get; set; }
        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }


        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }
    }
    public class StartupDeatailModelList
    {
        public int StartupId { get; set; }

        public string? StartUpName { get; set; }

        public string? Address { get; set; }

        public string? CompanyLegalName { get; set; }

        public string? CompanyContact { get; set; }

        public string? CompanyEmailId { get; set; }

        public string? CompanyHeadquartersAddress { get; set; }

        public string? FoundingYear { get; set; }


        public string? WebsiteUrl { get; set; }

        public byte[]? Logo { get; set; }

        public string? LogoFileName { get; set; }
        public string? EmployeeCount { get; set; }

        public string? CompanyDescription { get; set; }

        public string? ServiceDescription { get; set; }

        public string? BusinessModel { get; set; }

        public string? TargetCustomerBase { get; set; }

        public string? TargetMarket { get; set; }

        public string? ManagementInfo { get; set; }

        public bool IsStealth { get; set; }

        public int SectorId { get; set; }
        public string? SectorName { get; set; }
        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }
    }
}

