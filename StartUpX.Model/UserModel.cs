using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public int FounderTypeId { get; set; }
        public string? FounderName { get; set; }
        public string? RoleName { get; set; }
        public int RoleId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CretedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpadteDate { get; set; }

        public int? UpadateBy { get; set; }
        public bool? ServiceStatus { get; set; }
        public string? Category { get; set; }

        public ServiceModel? serviceDataModel { get; set; }
    }
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class UserRegister
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string message { get; set; }

    }
    public class UserActivateModel
    {
        public string EncryptedUserId { get; set; }

    }
    public class ChangePassword
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
    public class ServiceProviderUserModel
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public int FounderTypeId { get; set; }
        public string? FounderName { get; set; }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CretedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpadteDate { get; set; }

        public int? UpadateBy { get; set; }
        public int? LoggedUserId { get; set; }
       
         public bool? ServiceStatus { get; set; }
       public string? Category { get; set; }
    }
    public class ServiceStatusUpdateModel
    {
        public bool IsActive { get; set; }
        public bool? ServiceStatus { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }

        public int? LoggedUserId { get; set; }
    }

    public class CategoryModel1
    {
       public string Category { get; set; }
       

        public int UserId { get; set; }
    }
}
