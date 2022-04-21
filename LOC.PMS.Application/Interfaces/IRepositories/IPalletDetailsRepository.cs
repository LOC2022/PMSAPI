using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IPalletDetailsRepository
    {
        Task<IEnumerable<PalletDetails>> AddPalletDetails(PalletDetails palletDetailsRequest);
        
        Task<string> ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task<int> ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> SelectPalletDetails(string palletId);

        Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId);

        Task DeactivatePallets(string palletId);

        Task DeactivatePalletLocation(int locationId);

    }
}