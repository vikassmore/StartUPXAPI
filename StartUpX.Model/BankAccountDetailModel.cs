using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class BankAccountDetailModel
    {
        public int BankId { get; set; }

        public string? Ifsccode { get; set; }

        public string? BankName { get; set; }

        public string? BranchName { get; set; }

        public string? AccountNumber { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }

    }
}
