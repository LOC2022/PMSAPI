using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class PalletRepository : IPalletRepository
    {
        private readonly IContext _context;

        public PalletRepository(IContext context)
        {
            _context = context;
        }

        public Task InsertPallets(PalletDetailsRequest palletDetailsRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletPartNo", palletDetailsRequest.PalletPartNo),
                new SqlParameter("@PalletName", palletDetailsRequest.PalletName),
                new SqlParameter("@PalletWeight", palletDetailsRequest.PalletWeight),
                new SqlParameter("@Model", palletDetailsRequest.Model),
                new SqlParameter("@KitUnit", palletDetailsRequest.KitUnit),
                new SqlParameter("@WhereUsed", palletDetailsRequest.WhereUsed),
                new SqlParameter("@PalletType", palletDetailsRequest.PalletType),
                new SqlParameter("@ModifiedBy", palletDetailsRequest.ModifiedBy),
                new SqlParameter("@IsActive", palletDetailsRequest.IsActive)
            };

          _context.ExecuteStoredProcedure("[dbo].[PalletMaster_Insert]", sqlParams.ToArray());
            return Task.CompletedTask;
        }
    }
}