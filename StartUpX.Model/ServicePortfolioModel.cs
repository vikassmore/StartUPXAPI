using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ServicePortfolioModel
    {
        public int ServicePortFolioId { get; set; }

        public string? DocumentName { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public int ServiceId { get; set; }

        public int? LoggedUserId { get; set; }

    }
}
