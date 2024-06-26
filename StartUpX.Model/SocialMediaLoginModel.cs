using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SocialMediaLoginModel
    {

        public int SocialId { get; set; }

        public int UserId { get; set; }

        public string? Provider { get; set; }

        public string? ProviderId { get; set; }

        public string? AccessToken { get; set; }

        public bool IsActive { get; set; }
    }
    public class SocialMediaUserLoginModel
    {
        public int SocialId { get; set; }

        public int UserId { get; set; }

        public string? Provider { get; set; }

        public string? ProviderId { get; set; }

        public string? AccessToken { get; set; }

        public bool IsActive { get; set; }
    

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int RoleId { get; set; }

        public string? EmailId { get; set; }
        public int FounderTypeId { get; set; }

    }
}
