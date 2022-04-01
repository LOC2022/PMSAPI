using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class PalletDetailsRepository : IPalletDetailsRepository
    {
        private readonly IContext _context;

        public PalletDetailsRepository(IContext context)
        {
            _context = context;
        }

        public async Task<int> ModifyPalletDetails(PalletDetails palletDetailsRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletPartId", palletDetailsRequest.PalletId),
                new SqlParameter("@PalletPartNo", palletDetailsRequest.PalletPartNo),
                new SqlParameter("@PalletName", palletDetailsRequest.PalletName),
                new SqlParameter("@PalletWeight", palletDetailsRequest.PalletWeight),
                new SqlParameter("@Model", palletDetailsRequest.Model),
                new SqlParameter("@KitUnit", palletDetailsRequest.KitUnit),
                new SqlParameter("@WhereUsed", palletDetailsRequest.WhereUsed),
                new SqlParameter("@PalletType", palletDetailsRequest.PalletType),
                new SqlParameter("@LocationId", palletDetailsRequest.LocationId),
                new SqlParameter("@D2LDays", palletDetailsRequest.D2LDays),
                new SqlParameter("@Availability", palletDetailsRequest.Availability),
                new SqlParameter("@CreatedBy", palletDetailsRequest.CreatedBy),
                new SqlParameter("ReturnPalletPartId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

           return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[PalletMaster_Modify]", sqlParams.ToArray());
        }

        public async Task<int> ModifyPalletLocation(LocationMaster palletLocation)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", palletLocation.LocationId),
                new SqlParameter("@Location", palletLocation.Location),
                new SqlParameter("@IsActive", palletLocation.IsActive),
                new SqlParameter("@CreatedBy", palletLocation.CreatedBy),
                new SqlParameter("ReturnLocationId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

            return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[PalletLocationMaster_Modify]", sqlParams.ToArray());

        }

        public async Task<IEnumerable<PalletDetails>> SelectPalletDetails(int palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId)
            };

            return await _context.QueryStoredProcedureAsync<PalletDetails>("[dbo].[PalletMaster_Select]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", locationId)
            };

            return await  _context.QueryStoredProcedureAsync<LocationMaster>("[dbo].[PalletLocationMaster_Select]", sqlParams.ToArray());
        }

        public Task DeactivatePallets(int palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId)
            };
            _context.ExecuteStoredProcedure("[dbo].[PalletMaster_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task DeactivatePalletLocation(int locationId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", locationId)
            };
            _context.ExecuteStoredProcedure("[dbo].[PalletLocationMaster_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }
    }
}