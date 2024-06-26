using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class StateModel
    {
        public int StateId { get; set; }

        public string? StateName { get; set; }

        public string? StateCode { get; set; }

        public int? CountryId { get; set; }

        public bool IsActive { get; set; }
    }
}
