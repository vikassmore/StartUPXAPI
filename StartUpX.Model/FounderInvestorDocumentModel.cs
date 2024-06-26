using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FounderInvestorDocumentModel
    {
        public int DocumentId { get; set; }

        public string? DocumentName { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public int UserId { get; set; }

        public int? LoggedUserId { get; set; }
    }

}
