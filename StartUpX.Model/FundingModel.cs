using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FundingModel
    {
        public int FundingId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CretedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpadteDate { get; set; }

        public int? UpadateBy { get; set; }
        public int? LoggedUserId { get; set; }
    }
}
