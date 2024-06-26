using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FundingDetailsModel
    {
        public int FundingDetailsId { get; set; }

        public string? SeriesName { get; set; }

        public string? ShareClass { get; set; }

        public string? DateFinancing { get; set; }

        public string? SharesOutstanding { get; set; }

        public string? IssuePrice { get; set; }

        public string? ConversionPrice { get; set; }

        public string? TotalFinancingSize { get; set; }

        public string? LiquidityRank { get; set; }

        public string? LiquidationPreference { get; set; }

        public string? DividendRate { get; set; }

        public string? DividendType { get; set; }

        public string? VotesPerShare { get; set; }

        public string? RedemptionRights { get; set; }

        public string? ConvertibleToOnPublicListing { get; set; }

        public string? ParticipatingPreferred { get; set; }

        public string? QualifiedIpo { get; set; }

        public string? OtherKeyProvisions { get; set; }

        public int FundingId { get; set; }

        public bool IsActive { get; set; }
        public int LoggedUserId { get; set; }
    }
}

