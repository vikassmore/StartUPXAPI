using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class AccreditedInvestorModel
    {
        public int AccreditedInvestorId { get; set; }

        public string? AccreditedInvestorName { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }
        public int? LoggedUserId { get; set; }
    }
}
