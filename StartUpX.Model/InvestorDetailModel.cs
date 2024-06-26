using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class InvestorDetailModel
    {
        public int InvestorId { get; set; }

        public string? ProfileType { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }

        public int FounderTypeId { get; set; }
        public string? FounderTypeName { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int StateId { get; set; }

        public string? StateName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int? ZipCode { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }


    }
    public class InvestorDetailModelList
    {
        public int InvestorId { get; set; }

        public string? ProfileType { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }

        public string? MobileNo { get; set; }
        public int FounderTypeId { get; set; }
        public string? FounderTypeName { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int StateId { get; set; }

        public string? StateName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int? ZipCode { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }




        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }
    }
}
