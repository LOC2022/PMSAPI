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
            string VendorQry = @"select DISTINCT DP.VendorId,DP.OrderDate,COUNT(distinct DP.PalletPartNo) ReqPart,PM.D2LDays,VM.NonD2LDays from [dbo].[DayPlan] DP
                                LEFT JOIN PalletMaster PM on DP.PalletPartNo=PM.PalletPartNo
                                LEFT JOIN VendorMaster VM on VM.VendorId=DP.VendorId
                                where DP.IsActive = 1
                                GROUP BY DP.VendorId,DP.OrderDate,PM.D2LDays,VM.NonD2LDays";

            var dt = _context.QueryData<DayPlan>(VendorQry);
            List<Orders> OrderList = new List<Orders>();
            List<PalletsByOrderTrans> palletsByOrderTrans = new List<PalletsByOrderTrans>();

            foreach (var d in dt)
            {
                DateTime OrderDate;
                if (d.D2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.D2LDays);
                else if (d.NonD2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.NonD2LDays);
                else
                    OrderDate = d.OrderDate;

                if (OrderDate.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy"))
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
                              FROM[PalletMaster] Where PalletPartNo = '{d1.PalletPartNo}' and Availability= {(int)PalletAvailability.Ideal}";

                            var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();


                            OrderList.Add(new Orders()
                            {
                                OrderNo = OrderId,
                                VendorId = d1.VendorId,
                                NoOfPartsOrdered = int.Parse(d.ReqPart),
                                OrderQty = d1.Qty,
                                OrderTypeId = 1,
                                OrderStatusId = OrderStatus.Open,
                                OrderCreatedDate = DateTime.Now
                            });

                            if (PalletList.Count > 0)
                            {
                                foreach (var d2 in PalletList)
                                {
                                    palletsByOrderTrans.Add(new PalletsByOrderTrans()
                                    {
                                        OrderNo = OrderId,
                                        PalletId = d2.PalletId,
                                        AssignedQty = d1.Qty,
                                        LocationId = d2.LocationId,
                                        PalletStatus = PalletStatus.Assigned,
                                        ModifiedDate = DateTime.Now
                                    });
                                }
                            }

                        }
                    }                   

                    string UpdateQry = $"UPDATE DayPlan SET IsActive=0 WHERE IsActive = 1 and VendorId = '{d.VendorId}' and OrderDate = '{d.OrderDate}'";
                    _context.ExecuteSql(UpdateQry);
                }

            }
            if (OrderList.Count > 0)
                _context.BulkCopy(OrderList, OrderList.Count, true);
            if (palletsByOrderTrans.Count > 0)
                _context.BulkCopy(palletsByOrderTrans, palletsByOrderTrans.Count, true);

            var str = String.Join(",", palletsByOrderTrans.Select(x=>x.PalletId));
            string UpdatePalletQry = $"UPDATE PalletMaster SET Availability=2 WHERE Availability = 1 and PalletId IN ({str})";
            _context.ExecuteSql(UpdatePalletQry);


        }




    }
}