using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class AuthModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Token { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string FounderName { get; set; }
        public string FounderTypeId { get; set; }
        
    }
}
