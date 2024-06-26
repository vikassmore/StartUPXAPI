using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SuitabilityQuestionModel
    {
        public int SquestionId { get; set; }

        public string? MaximumInvestent { get; set; }

        public string? AcceptInvestment { get; set; }

        public string? ReleventInvestment { get; set; }

        public string? LiquidWorth { get; set; }

        public string? RiskFector { get; set; }

        public string? ConfiditialAgreement { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public int? LoggedUserId { get; set; }
    }
}
