using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SectorModel
    {
        public int SectorId { get; set; }

        public string? SectorName { get; set; }

        public string? SubSectorName { get; set; }

        public string? SectorDescription { get; set; }

        public bool IsActive { get; set; }

        public int LoggedUserId { get; set; }
    }
}
