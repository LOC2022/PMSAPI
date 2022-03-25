using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletDetailsProvider
    {
        Task ModifyPalletDetails(PalletDetails palletDetailsRequest);

        Task ModifyPalletLocation(LocationMaster palletLocation);

        Task<IEnumerable<PalletDetails>> GetPalletDetails(int palletId);

        Task<IEnumerable<LocationMaster>> GetPalletLocation(int locationId);

        Task DeletePalletByPalletId(int palletId);

        Task DeletePalletLocationById(int locationId);

    }
}