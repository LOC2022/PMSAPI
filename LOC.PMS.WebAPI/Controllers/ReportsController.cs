using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using LOC.PMS.Model.Report;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportDetailsProvider _reportDetailsProvider;
        private const string GETALL = "";
        private const int DefaultValue = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDetailsProvider"></param>
        public ReportsController(IReportDetailsProvider reportDetailsProvider)
        {
            this._reportDetailsProvider = reportDetailsProvider;
        }

        /// <summary>
        /// GetMonthlyPlanReport
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="VendorId"></param>        
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetMonthlyPlanReport",
            Tags = new[] { "GetMonthlyPlanReport" },
            OperationId = "GetMonthlyPlanReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetMonthlyPlanReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetMonthlyPlanReport([FromQuery] string FromDate = GETALL, [FromQuery] string ToDate = GETALL, [FromQuery] int VendorId = DefaultValue)
        {
            var response = await _reportDetailsProvider.GetMonthlyPlanReport(FromDate, ToDate, VendorId);
            return Ok(response);
        }

        /// <summary>
        /// GetDCDetailsReport
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="DCNumber"></param>        
        /// <returns>List of DCs</returns>
        [SwaggerOperation(
            Description = "GetDCDetailsReport",
            Tags = new[] { "GetDCDetailsReport" },
            OperationId = "GetDCDetailsReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDCDetailsReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDCDetailsReport([FromQuery] string FromDate = GETALL, [FromQuery] string ToDate = GETALL, [FromQuery] string DCNumber = null, [FromQuery] string UserId = GETALL, [FromQuery] string VendorId = GETALL)
        {
            var response = DCNumber != null ? await _reportDetailsProvider.GetDCDetails(DCNumber) : await _reportDetailsProvider.GetDCDetailsReport(FromDate, ToDate, VendorId);
            return Ok(response);
        }


        /// <summary>
        /// GetDayPlanReport
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>      
        /// <returns>List of Dayplan reports</returns>
        [SwaggerOperation(
            Description = "GetDayPlanReport",
            Tags = new[] { "GetDayPlanReport" },
            OperationId = "GetDayPlanReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDayPlanReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDayPlanReport([FromQuery] string FromDate = GETALL, [FromQuery] string ToDate = GETALL)
        {
            var response = await _reportDetailsProvider.GetDayPlanReport(FromDate, ToDate);
            return Ok(response);
        }

        /// <summary>
        /// GetMonthlyPlanReport
        /// </summary>
        /// <param name="PalletId"></param>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "PalletByOrderTransReport",
            Tags = new[] { "PalletByOrderTransReport" },
            OperationId = "PalletByOrderTransReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("PalletByOrderTransReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> PalletByOrderTransReport([FromQuery] string PalletId = GETALL)
        {
            var response = await _reportDetailsProvider.GetPalletOrderTransReport(PalletId);
            return Ok(response);
        }

        /// <summary>
        /// PalletPartDetails
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PalletPartNo"></param>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "PalletPartTransReport",
            Tags = new[] { "PalletPartTransReport" },
            OperationId = "PalletPartTransReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("PalletPartTransReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> PalletPartTransReport([FromQuery] int UserId = DefaultValue, [FromQuery] string PalletPartNo = GETALL)
        {
            var response = await _reportDetailsProvider.GetPalletPartTransReport(UserId, PalletPartNo);
            return Ok(response);
        }


        /// <summary>
        /// GetVendorInwardDetails
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DCNumber"></param>    
        /// <param name="PartNumber"></param> 
        /// <param name="PalletStatus"></param> 
        /// <returns>List of Inward DC details</returns>
        [SwaggerOperation(
            Description = "GetVendorInwardDetails",
            Tags = new[] { "GetVendorInwardDetails" },
            OperationId = "GetVendorInwardDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetVendorInwardDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetVendorInwardDetails([FromQuery] int UserId = DefaultValue, [FromQuery] string DCNumber = GETALL, [FromQuery] string PartNumber = GETALL, int PalletStatus = DefaultValue)
        {
            if (PartNumber != GETALL)
            {
                var response = await _reportDetailsProvider.GetInwardReportByPartNumber(UserId, PartNumber, PalletStatus);
                return Ok(response);
            }
            else if (DCNumber == GETALL)
            {
                var response = await _reportDetailsProvider.GetInwardReport(UserId, PalletStatus);
                return Ok(response);
            }
            else
            {
                var response = await _reportDetailsProvider.GetInwardReportByDCNumber(DCNumber, PalletStatus);
                return Ok(response);
            }
        }


        /// <summary>
        /// WareHouseTransitAndStockDetails
        /// </summary>   
        /// <param name="status"></param> 
        /// <returns>List of WareHouseStockDetails</returns>
        [SwaggerOperation(
            Description = "WareHouseStockDetails",
            Tags = new[] { "WareHouseStockDetails" },
            OperationId = "WareHouseStockDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("WareHouseStockDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> WareHouseStockDetails([FromQuery] string status)
        {
            var response = await _reportDetailsProvider.WareHouseStockDetails(status);
            return Ok(response);
        }


        /// <summary>
        /// WareHouseTransitDetails
        /// </summary>   
        /// <param name="status"></param> 
        /// <param name="DCNumber"></param> 
        /// <param name="PartNumber"></param> 
        /// <returns>List of WareHouseTransitDetails</returns>
        [SwaggerOperation(
            Description = "WareHouseTransitDetails",
            Tags = new[] { "WareHouseTransitDetails" },
            OperationId = "WareHouseTransitDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("WareHouseTransitDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> WareHouseTransitDetails([FromQuery] string status, [FromQuery] string DCNumber = GETALL, [FromQuery] string PartNumber = GETALL)
        {
            var response = await _reportDetailsProvider.WareHouseTransitDetails(status, DCNumber, PartNumber);
            return Ok(response);
        }


        /// <summary>
        /// GetPalletDropDownSelection
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PalletStatus"></param>    
        /// <param name="ModelNo"></param> 
        /// <returns>Selection details</returns>
        [SwaggerOperation(
            Description = "GetPalletDropDownSelection",
            Tags = new[] { "GetPalletDropDownSelection" },
            OperationId = "GetPalletDropDownSelection")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletDropDownSelection"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPalletDropDownSelection([FromQuery] int UserId = DefaultValue, [FromQuery] string PalletStatus = GETALL, [FromQuery] string ModelNo = GETALL)
        {
            var response = await _reportDetailsProvider.GetPalletReportSelection(UserId, PalletStatus, ModelNo);
            return Ok(response);
        }

        /// <summary>
        /// GetDayPlanReport
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>      
        /// <returns>List of Dayplan reports</returns>
        [SwaggerOperation(
            Description = "GetDateWiseOrder",
            Tags = new[] { "GetDateWiseOrder" },
            OperationId = "GetDateWiseOrder")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDateWiseOrder"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDateWiseOrder([FromQuery] string FromDate = GETALL, [FromQuery] string ToDate = GETALL)
        {
            var response = await _reportDetailsProvider.GetDateWiseOrder(FromDate, ToDate);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderDate"></param>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Get Order Details.",
            Tags = new[] { "GetOrderDetailsByDate" },
            OperationId = "GetOrderDetailsByDate")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetOrderDetailsByDate"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetOrderDetailsByDate([FromQuery] string OrderDate = GETALL)
        {
            var response = await _reportDetailsProvider.GetOrderDetailsByDate(OrderDate);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>

        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Get Pallet Details.",
            Tags = new[] { "GetDBPalletCount" },
            OperationId = "GetDBPalletCount")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDBPalletCount"), MapToApiVersion("1.0")]
        public IActionResult GetDBPalletCount([FromQuery] string UserId = GETALL)
        {
            var response = _reportDetailsProvider.GetDBPalletCount(UserId);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>

        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Get Pallet Details.",
            Tags = new[] { "GetDBPalletCountByType" },
            OperationId = "GetDBPalletCountByType")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDBPalletCountByType"), MapToApiVersion("1.0")]
        public IActionResult GetDBPalletCountByType([FromQuery] string DBType = GETALL, [FromQuery] string UserId = GETALL)
        {

            if (DBType == "InTransit")
            {
                var response = _reportDetailsProvider.GetDBPalletCountByTypeInTransit(UserId);
                return Ok(response);

            }
            if (DBType == "OnSite")
            {

                var response = _reportDetailsProvider.GetDBPalletCountByTypeOnSite(UserId);
                return Ok(response);


            }
            if (DBType == "Maintenance")
            {
                var response = _reportDetailsProvider.GetDBPalletCountByTypeMaintenance(UserId);
                return Ok(response);
            }


            return Ok("");
        }

        /// <summary>
        /// 
        /// </summary>

        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetDBInTransit.",
            Tags = new[] { "GetDBInTransit" },
            OperationId = "GetDBInTransit")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDBInTransit"), MapToApiVersion("1.0")]
        public IActionResult GetDBInTransit([FromQuery] string Type = GETALL, [FromQuery] string UserId = GETALL)
        {

            if (Type == "A-S")
            {
                var response = _reportDetailsProvider.GetDBInTransit_AS(UserId);
                return Ok(response);

            }
            if (Type == "S-C")
            {
                var response = _reportDetailsProvider.GetDBInTransit_SC(UserId);
                return Ok(response);
            }

            if (Type == "C-A")
            {
                var response = _reportDetailsProvider.GetDBInTransit_CA(UserId);
                return Ok(response);
            }


            return Ok("");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetPalletDetailsByPart.",
            Tags = new[] { "GetPalletDetailsByPart" },
            OperationId = "GetPalletDetailsByPart")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletDetailsByPart"), MapToApiVersion("1.0")]
        public IActionResult GetPalletDetailsByPart([FromQuery] string PalletPartNo = GETALL, [FromQuery] string UserId = GETALL, [FromQuery] string Status = "")
        {
            var response = _reportDetailsProvider.GetPalletDetailsByPart(UserId, Status, PalletPartNo);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Supplier Plan In And Out Flow",
            Tags = new[] { "Supplier Plan In And Out Flow" },
            OperationId = "Supplier Plan In And Out Flow")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetSupplierInOutFlowDB"), MapToApiVersion("1.0")]
        public IActionResult GetSupplierInOutFlowDB([FromQuery] string UserId, [FromQuery] string StartDate = GETALL, [FromQuery] string EndDate = "")
        {
            var response = _reportDetailsProvider.GetSupplierInOutFlowDB(UserId, StartDate, EndDate);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetPlannedDBDetails",
            Tags = new[] { "GetPlannedDBDetails" },
            OperationId = "GetPlannedDBDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPlannedDBDetails"), MapToApiVersion("1.0")]
        public IActionResult GetPlannedDBDetails([FromQuery] string UserId, [FromQuery] string StartDate = GETALL, [FromQuery] string EndDate = "")
        {
            var response = _reportDetailsProvider.GetPlannedDBDetails(UserId, StartDate, EndDate);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Trip Details",
            Tags = new[] { "Trip Details" },
            OperationId = "Trip Details")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetTripDBDetails"), MapToApiVersion("1.0")]
        public IActionResult GetTripDBDetails([FromQuery] string StartDate = GETALL, [FromQuery] string EndDate = "")
        {
            var response = _reportDetailsProvider.GetTripDBDetails(StartDate, EndDate);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Aging Details",
            Tags = new[] { "Aging Details" },
            OperationId = "Aging Details")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletAgingDB"), MapToApiVersion("1.0")]
        public IActionResult GetPalletAgingDB([FromQuery] string UserId = GETALL, [FromQuery] string Aging = "")
        {
            var response = _reportDetailsProvider.GetPalletAgingDB(UserId, Aging);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetDcNoForManual",
            Tags = new[] { "GetDcNoForManual" },
            OperationId = "GetDcNoForManual")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDcNoForManual"), MapToApiVersion("1.0")]
        public IActionResult GetDcNoForManual([FromQuery] string VendorId = GETALL, [FromQuery] string Flag = "")
        {
            var response = _reportDetailsProvider.GetDcNoForManual(VendorId, Flag);
            return Ok(response);
        }

        //Manual Process

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetDcDetailsForInward",
            Tags = new[] { "GetDcDetailsForInward" },
            OperationId = "GetDcDetailsForInward")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDcDetailsForInward"), MapToApiVersion("1.0")]
        public IActionResult GetDcDetailsForInward([FromQuery] string DCNo = GETALL)
        {
            var response = _reportDetailsProvider.GetDcDetailsForInward(DCNo);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "SaveInwardDetails",
            Tags = new[] { "SaveInwardDetails" },
            OperationId = "SaveInwardDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SaveInwardDetails"), MapToApiVersion("1.0")]
        public IActionResult SaveInwardDetails([FromBody] List<string> lstPallets, [FromQuery] string DCNo)
        {
            _reportDetailsProvider.SaveInwardDetails(lstPallets, DCNo);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "SaveDispatchDetails",
            Tags = new[] { "SaveDispatchDetails" },
            OperationId = "SaveDispatchDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SaveDispatchDetails"), MapToApiVersion("1.0")]
        public IActionResult SaveDispatchDetails([FromBody] List<DCDetails> lstPallets, [FromQuery] string VendorId)
        {
            _reportDetailsProvider.SaveDispatchDetails(lstPallets, VendorId);
            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>ValidatePalletId</returns>
        [SwaggerOperation(
            Description = "ValidatePalletId",
            Tags = new[] { "ValidatePalletId" },
            OperationId = "ValidatePalletId")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("ValidatePalletId"), MapToApiVersion("1.0")]
        public IActionResult ValidatePalletId([FromQuery] string PalletId)
        {
            return Ok(_reportDetailsProvider.ValidatePalletId(PalletId));
        }

        //GetOrderNoForManual

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetOrderNoForManual",
            Tags = new[] { "GetOrderNoForManual" },
            OperationId = "GetOrderNoForManual")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetOrderNoForManual"), MapToApiVersion("1.0")]
        public IActionResult GetOrderNoForManual([FromQuery] string VendorId = GETALL)
        {
            var response = _reportDetailsProvider.GetOrderNoForManual(VendorId);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetOrderDetailsForDispatch",
            Tags = new[] { "GetOrderDetailsForDispatch" },
            OperationId = "GetOrderDetailsForDispatch")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetOrderDetailsForDispatch"), MapToApiVersion("1.0")]
        public IActionResult GetOrderDetailsForDispatch([FromQuery] string OrderNo = GETALL)
        {
            var response = _reportDetailsProvider.GetOrderDetailsForDispatch(OrderNo);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GenerateDCForManual",
            Tags = new[] { "GenerateDCForManual" },
            OperationId = "GenerateDCForManual")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("GenerateDCForManual"), MapToApiVersion("1.0")]
        public IActionResult GenerateDCForManual([FromBody] List<string> lstPallets, [FromQuery] string OrderNo = GETALL)
        {
            _reportDetailsProvider.GenerateDCForManual(lstPallets, OrderNo);
            return Ok();
        }

        //GetOrderNoForDispatch

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetOrderNoForDispatch",
            Tags = new[] { "GetOrderNoForDispatch" },
            OperationId = "GetOrderNoForDispatch")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetOrderNoForDispatch"), MapToApiVersion("1.0")]
        public IActionResult GetOrderNoForDispatch()
        {
            return Ok(_reportDetailsProvider.GetOrderNoForDispatch());
        }

        //DispatchOrder

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "DispatchOrder",
            Tags = new[] { "DispatchOrder" },
            OperationId = "DispatchOrder")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("DispatchOrder"), MapToApiVersion("1.0")]
        public IActionResult DispatchOrder([FromBody] List<string> lstPallets, [FromQuery] string OrderNo = GETALL)
        {
            _reportDetailsProvider.DispatchOrder(lstPallets, OrderNo);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>DC Basic Details</returns>
        [SwaggerOperation(
            Description = "GetAddtionalDCDetails",
            Tags = new[] { "GetAddtionalDCDetails" },
            OperationId = "GetAddtionalDCDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetAddtionalDCDetails"), MapToApiVersion("1.0")]
        public IActionResult GetAddtionalDCDetails([FromQuery] string DCNo)
        {
            return Ok(_reportDetailsProvider.GetAddtionalDCDetails(DCNo));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns>FullCycle Report Data</returns>
        [SwaggerOperation(
            Description = "GetFullCycleReport",
            Tags = new[] { "GetFullCycleReport" },
            OperationId = "GetFullCycleReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetFullCycleReport"), MapToApiVersion("1.0")]
        public IActionResult GetFullCycleReport()
        {
            return Ok(_reportDetailsProvider.GetFullCycleReport());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Save Mail Details</returns>
        [SwaggerOperation(
            Description = "SaveMailDetails",
            Tags = new[] { "SaveMailDetails" },
            OperationId = "SaveMailDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SaveMailDetails"), MapToApiVersion("1.0")]
        public IActionResult SaveMailDetails([FromBody] MailModel mailModel)
        {
            _reportDetailsProvider.SaveMailDetails(mailModel);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Mail Details</returns>
        [SwaggerOperation(
            Description = "GetMailDetails",
            Tags = new[] { "GetMailDetails" },
            OperationId = "GetMailDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetMailDetails"), MapToApiVersion("1.0")]
        public IActionResult GetMailDetails()
        {

            return Ok(_reportDetailsProvider.GetMailDetails());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Delete Mail Details</returns>
        [SwaggerOperation(
            Description = "DeleteMailDetails",
            Tags = new[] { "DeleteMailDetails" },
            OperationId = "DeleteMailDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("DeleteMailDetails"), MapToApiVersion("1.0")]
        public IActionResult DeleteMailDetails([FromQuery] string Id)
        {
            _reportDetailsProvider.DeleteMailDetails(Id);
            return Ok();
        }
    }
}
