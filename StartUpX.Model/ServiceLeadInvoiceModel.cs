using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ServiceLeadInvoiceModel
    {
        public int ServiceLeadInvoiceId { get; set; }

        public string? InvoiceFileName { get; set; }

        public string InvoiceFilePath { get; set; }

        public int ServiceCaseId { get; set; }

        public int LoggedUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
