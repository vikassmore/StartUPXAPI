using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class StartUpCategoryModel
    {
        public int StartUpCategoryId { get; set; }

        public string? Name { get; set; }

        public string? Discription { get; set; }

        public bool IsActive { get; set; }
    }
}
