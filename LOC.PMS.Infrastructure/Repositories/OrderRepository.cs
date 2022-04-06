using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Newtonsoft.Json;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;

        public OrderRepository(IContext context)
        {
            _context = context;
        }



        public Task AddDayPlanData(List<DayPlan> order)
        {
            _context.BulkCopy(order, order.Count, true);
            CreateOrder();
            return Task.CompletedTask;
        }

        public void CreateOrder()
        {
            string VendorQry = @"select distinct VendorId,OrderDate,COUNT(distinct PalletPartNo) ReqPart  from [dbo].[DayPlan]
                                where IsActive = 1
                                GROUP BY VendorId,OrderDate";

            var dt = _context.QueryData<DayPlan>(VendorQry);
            foreach (var d in dt)
            {
                var OrderId = "ORD" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string sql = @$"select SUM(RequiredQty) Qty,PalletPartNo,Description PalletPartName,VendorId,OrderDate from [dbo].[DayPlan]
                            where IsActive = 1 and VendorId='{d.VendorId}' and OrderDate='{d.OrderDate}'
                            Group by PalletPartNo,Description,VendorId,OrderDate ";
                var Data = _context.QueryData<DayPlan>(sql);

                foreach (var d1 in Data)
                {
                    var PalletSize = 1;
                    string strPalletWeight = @$"select top 1 KitUnit from PalletMaster where PalletPartNo='{d1.PalletPartNo}'";
                    PalletSize = _context.QueryData<int>(strPalletWeight).FirstOrDefault();
                    if (PalletSize != 0)
                    {
                        var ReqPallet = (int)(d1.Qty / PalletSize);
                        var Pallets = @"SELECT TOP  " + ReqPallet + @$"[PalletId]
                                  ,[PalletPartNo]
                                  ,[PalletName]
                                  ,[PalletWeight]
                                  ,[Model]
                                  ,[KitUnit]
                                  ,[WhereUsed]
                                  ,[PalletType]
                                  ,[LocationId]
                              FROM[PalletMaster] Where PalletPartNo = '{d1.PalletPartNo}'";

                        var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();

                        string InsertOrder = @"INSERT INTO [Orders]([OrderNo],[VendorId],[NoOfPartsOrdered],[OrderQty],[OrderTypeId],[OrderStatusId],[OrderCreatedDate]) Values";
                        InsertOrder += $"('{OrderId}','{d.VendorId}','{d.ReqPart}','{d1.Qty}',1,1,GETDATE())";


                        _context.ExecuteSql(InsertOrder);

                        if (PalletList.Count > 0)
                        {
                            foreach (var d2 in PalletList)
                            {
                                string InsertPallet = @$"INSERT INTO PalletsByOrderTrans
                               ([OrderNo]
                               ,[PalletId]
                               ,[AssignedQty]
                               ,[LocationId]
                               ,[PalletStatus]
                               ,[ModifiedDate])
                                VALUES ('{OrderId}','{d2.PalletId}','{d1.Qty}','{d2.LocationId}',1,GETDATE())";

                                _context.ExecuteSql(InsertPallet);
                            }
                        }

                    }



                }

            }



        }




    }
}