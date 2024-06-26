using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FAQModel
    {
        public int FrequentlyAqid { get; set; }

        public string? Question { get; set; }

        public string? Answer { get; set; }
        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }

    }
}
