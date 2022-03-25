using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IPalletRepository
    {
        Task ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> SelectPalletDetails(int palletId);

        Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId);

        Task DeletePallets(int palletId);

        Task DeletePalletLocation(int locationId);

    }
}