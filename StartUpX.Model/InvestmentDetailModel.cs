using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public class InvestmentDetailModel
    {
        public int InvestmentId { get; set; }

        public string? InvestmentStage { get; set; }

        public string? InvestmentSector { get; set; }

        public string? InvestmentAmount { get; set; }

        public bool IsActive { get; set; }
        public int LoggedUserId { get; set; }
    }
}
