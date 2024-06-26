using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class PolicyModel
    {
        public int PolicyId { get; set; }

        public string? PolicyName { get; set; }

        public string? PolicyDescription { get; set; }

        public int LoggedUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
