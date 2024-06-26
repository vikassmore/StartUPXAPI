using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CountryModel
    {
        public int CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? CountryCode { get; set; }

        public bool IsActive { get; set; }
    }
}
