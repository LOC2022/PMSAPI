using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IPalletRepository
    {
        Task InsertPallets(PalletDetails palletDetailsRequest);

        Task<IEnumerable<PalletDetails>> SelectPalletDetails(string palletPartNo);

        Task DeletePallets(int palletId);
    }
}