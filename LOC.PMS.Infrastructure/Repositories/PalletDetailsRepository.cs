using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public async Task<IEnumerable<PalletDetails>> AddPalletDetails(PalletDetails palletDetailsRequest)
        {
            var palletList = new List<PalletDetails>();

            var util = new Utilities.Utilities();

            for (int i = 1; i <= palletDetailsRequest.palletPartQty; i++)
            {
                palletList.Add(new PalletDetails()
                {
                    PalletId = "A" + util.CreateUnique16DigitString(),
                    PalletPartNo = palletDetailsRequest.PalletPartNo.Trim(),
                    PalletName = palletDetailsRequest.PalletName,
                    PalletWeight = palletDetailsRequest.PalletWeight,
                    Model = palletDetailsRequest.Model,
                    KitUnit = palletDetailsRequest.KitUnit,
                    WhereUsed = palletDetailsRequest.WhereUsed,
                    PalletType = palletDetailsRequest.PalletType,
                    LocationId = palletDetailsRequest.LocationId,
                    D2LDays = palletDetailsRequest.D2LDays,
                    Availability = PalletAvailability.Ideal,
                    CreatedDate = DateTime.Now,
                    CreatedBy = palletDetailsRequest.CreatedBy,
                    Dimensions = palletDetailsRequest.Dimensions,
                    Rate = palletDetailsRequest.Rate,
                    Assy = palletDetailsRequest.Assy

                });
            }

            var ColList = new List<string> { "PalletId", "PalletPartNo", "PalletName", "PalletWeight", "Model", "KitUnit", "WhereUsed", "PalletType", "LocationId", "D2LDays", "Availability", "WriteCount", "CreatedDate", "CreatedBy", "Dimensions", "Rate", "Assy" };

            _context.BulkCopy(palletList, ColList, 30, "PalletMaster");



            string UpdatePalletQry = $"update PalletMaster set D2LDays = {palletDetailsRequest.D2LDays}   WHERE PalletPartNo='{palletDetailsRequest.PalletPartNo.Trim()}' ;";
            _context.ExecuteSql(UpdatePalletQry);

            return await SelectPalletDetails(null);
        }

        public async Task<string> ModifyPalletDetails(PalletDetails palletDetailsRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletDetailsRequest.PalletId),
                new SqlParameter("@PalletPartNo", palletDetailsRequest.PalletPartNo.Trim()),
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
                new SqlParameter("@WriteCount", palletDetailsRequest.WriteCount),
                new SqlParameter("@Rate", Convert.ToInt32(palletDetailsRequest.Rate)),
                new SqlParameter("@Dimension", palletDetailsRequest.Dimensions)
            };

            await _context.ExecuteStoredProcedureAsync("[dbo].[PalletMaster_Modify]", sqlParams.ToArray());

            return palletDetailsRequest.PalletId;
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

        public async Task<IEnumerable<PalletDetails>> SelectPalletDetails(string palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId ?? "ALL")
            };

            return await _context.QueryStoredProcedureAsync<PalletDetails>("[dbo].[PalletMaster_Select]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<LocationMaster>> SelectPalletLocation(int locationId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@LocationId", locationId)
            };

            return await _context.QueryStoredProcedureAsync<LocationMaster>("[dbo].[PalletLocationMaster_Select]", sqlParams.ToArray());
        }

        public Task DeactivatePallets(string palletId)
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