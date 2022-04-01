using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IPalletDetailsRepository
    {
        Task<int> ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task<int> ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> SelectPalletDetails(int palletId);

        Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId);

        Task DeactivatePallets(int palletId);

        Task DeactivatePalletLocation(int locationId);

    }
}