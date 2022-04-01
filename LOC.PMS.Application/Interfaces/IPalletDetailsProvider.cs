using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletDetailsProvider
    {
        Task<int> ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task<int> ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> GetPalletDetails(int palletId);

        Task<IEnumerable<LocationMaster>> GetPalletLocation(int locationId);

        Task DeactivatePalletByPalletId(int palletId);

        Task DeactivatePalletLocationById(int locationId);

    }
}