using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletDetailsProvider
    {
        Task<IEnumerable<PalletDetails>> AddPalletDetails(PalletDetails palletDetailsRequest);

        Task<string> ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task<int> ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> GetPalletDetails(string palletId);

        Task<IEnumerable<LocationMaster>> GetPalletLocation(int locationId);

        Task DeactivatePalletByPalletId(string palletId);

        Task DeactivatePalletLocationById(int locationId);

    }
}