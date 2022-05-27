using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        public async Task<IActionResult> GetDCDetailsReport([FromQuery] string FromDate = GETALL, [FromQuery] string ToDate = GETALL, [FromQuery] string DCNumber = null)
        {
            var response = DCNumber != null ? await _reportDetailsProvider.GetDCDetails(DCNumber) : await _reportDetailsProvider.GetDCDetailsReport(FromDate, ToDate);
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
    }
}
