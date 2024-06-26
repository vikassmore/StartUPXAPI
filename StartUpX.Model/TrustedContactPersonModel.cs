using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class TrustedContactPersonModel
    {
        public int TrustedContactId { get; set; }

        public string? Types { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }       

       
        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

         public string? MobileNo { get; set; }

        public string? ZipCode { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }

    }
}
