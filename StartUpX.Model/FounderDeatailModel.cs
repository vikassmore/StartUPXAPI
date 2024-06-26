using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FounderDeatailModel
    {
        public int FounderId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }

        public string? MobileNo { get; set; }

        public string? Gender { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public int LoggedUserId { get; set; }

    }

    public class AddFounder
    {
        public AddFounder()
        {
            this.Details = new List<FounderDeatailModel>();

        }

        public List<FounderDeatailModel> Details { get; set; }
    }

}

