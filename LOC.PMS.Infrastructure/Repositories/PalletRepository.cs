using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

        public Task ModifyPalletDetails(PalletDetails palletDetailsRequest)
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
                new SqlParameter("@Availability", palletDetailsRequest.Availability),
                new SqlParameter("@CreatedBy", palletDetailsRequest.CreatedBy)
            };

            _context.ExecuteStoredProcedure("[dbo].[PalletMaster_Modify]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task ModifyPalletLocation(LocationMaster palletLocation)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", palletLocation.LocationId),
                new SqlParameter("@Location", palletLocation.Location),
                new SqlParameter("@IsActive", palletLocation.IsActive),
                new SqlParameter("@CreatedBy", palletLocation.CreatedBy)

            };

            _context.ExecuteStoredProcedure("[dbo].[PalletLocationMaster_Modify]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PalletDetails>> SelectPalletDetails(int palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId)
            };

            return _context.QueryStoredProcedureAsync<PalletDetails>("[dbo].[PalletMaster_Select]", sqlParams.ToArray());
        }

        public Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", locationId)
            };

            return _context.QueryStoredProcedureAsync<LocationMaster>("[dbo].[PalletLocationMaster_Select]", sqlParams.ToArray());
        }

        public Task DeletePallets(int palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId)
            };
            _context.ExecuteStoredProcedure("[dbo].[PalletMaster_Delete]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task DeletePalletLocation(int locationId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", locationId)
            };
            _context.ExecuteStoredProcedure("[dbo].[PalletLocationMaster_Delete]", sqlParams.ToArray());
            return Task.CompletedTask;
        }
    }
}