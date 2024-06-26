using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISectorService
    {
        SectorModel GetSectorById(long sectorId, ref ErrorResponseModel errorResponseModel);
        List<SectorModel> GetAllSectors();
        string AddSectors(SectorModel sector, ref ErrorResponseModel errorResponseModel);
        string EditSector(SectorModel sector, ref ErrorResponseModel errorResponseModel);
        string DeleteSector(int sectorId,int userId, ErrorResponseModel errorResponseModel);
    }
}
