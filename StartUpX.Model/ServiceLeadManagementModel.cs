using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ServiceLeadManagementModel
    {
        public int ServiceCaseId { get; set; }

        public int ServiceId { get; set; }

        public DateTime? ServiceInterestDate { get; set; }

        public string? IntrestedServiceNames { get; set; }

        public string? Status { get; set; }

        public string? Comment { get; set; }

        public DateTime? AcceptedDate { get; set; }

        public int FounderUserId { get; set; }

        public int? LoggedUserId { get; set; }

        public bool IsActive { get; set; }
    }
    public class ServiceCaseModel
    {
        public int ServiceCaseId { get; set; }

        public int ServiceId { get; set; }

        public DateTime? ServiceInterestDate { get; set; }

        public string? IntrestedServiceNames { get; set; }

        public string? Status { get; set; }

        public string? Comment { get; set; }

        public DateTime? AcceptedDate { get; set; }

        public int FounderUserId { get; set; }

        public string? InvoiceFileName { get; set; }

        public string? InvoiceFilePath { get; set; }
        public int UserId { get; set; }


        public bool IsActive { get; set; }

        public int? InvoiceCount { get; set; }

        public StartupDeatailModelList StartupDeatailModel { get; set; }

        public ServiceModelData ServiceModel { get; set; }

    }
}
