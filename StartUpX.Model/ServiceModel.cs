using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }

        public string? ServiceProviderName { get; set; }

        public string? Category { get; set; }

        public string? ContactInformation { get; set; }

        public string? TagsKeywords { get; set; }

        public int? UserId { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }
    }
    public class ServiceModelData
    {
        public int ServiceId { get; set; }

        public string? ServiceProviderName { get; set; }

        public string? Category { get; set; }

        public string? ContactInformation { get; set; }

        public string? TagsKeywords { get; set; }

        public int? UserId { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }

        public List<ServicePortfolioModel> ServicePortfolioModel { get; set; }
    }
}
