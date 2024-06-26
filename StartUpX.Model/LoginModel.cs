using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        //public int roleId { get; set; }
    }

    public class OTPModel
    {
        public string? OTPNumber { get; set; }
        public int? UserId { get; set; }
        public DateTime? OTPValid { get; set; }


    }
    public class UserResponse
    {
        public string token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }
        public string FounderName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }

}
