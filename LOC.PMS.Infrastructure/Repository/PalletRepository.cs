using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Repository
{
    public class PalletRepository : IPalletRepository
    {
        private readonly IContext _context;

        public PalletRepository(IContext context)
        {
            _context = context;
        }

        public Task InsertPallet(PalletDetailsRequest palletDetailsRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletPartNo", palletDetailsRequest.PalletPartNo),
                new SqlParameter("@PalletName", palletDetailsRequest.PalletPartNo),
                new SqlParameter("@PalletWeight", palletDetailsRequest.PalletPartNo),
                new SqlParameter("@Model", palletDetailsRequest.PalletPartNo)
            };

            _context.ExecuteStoredProcedure("NewPalletsInsert", sqlParams.ToArray());

            return Task.CompletedTask;
        }
    }
}