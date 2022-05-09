using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDetailsProvider _transactionDetailsProvider;
        private const string GETALL = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionDetailsProvider"></param>
        public TransactionController(ITransactionDetailsProvider transactionDetailsProvider)
        {
            this._transactionDetailsProvider = transactionDetailsProvider;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "Get Order Details for HHT.",
            Tags = new[] { "GetHHTOrderDetails" },
            OperationId = "GetHHTOrderDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("GetHHTOrderDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetHHTOrderDetails(OrderDetails orderDetails)
        {
            var response = await _transactionDetailsProvider.GetHHTOrderDetails(orderDetails);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="DCStatus"></param>
        /// <param name="UserName"></param>
        /// <returns>List of DC's</returns>
        [SwaggerOperation(
            Description = "GetDCDetails.",
            Tags = new[] { "GetDCDetails" },
            OperationId = "GetDCDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDCDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDCDetails([FromQuery] string OrderNo = GETALL, [FromQuery] string DCStatus = null, [FromQuery] string UserName = null)
        {
            var response = await _transactionDetailsProvider.GetDCDetails(OrderNo, DCStatus, UserName);
            return Ok(response);
        }

        /// <summary>
        /// Generic Scan Update method
        /// </summary>
        /// <param name="PalletId"></param>
        /// <param name="ScannedQty"></param>
        /// <param name="ToStatus"></param>
        /// <param name="OrderNo"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Update Scan details.",
            Tags = new[] { "UpdateScanDetails" },
            OperationId = "UpdateScanDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("UpdateScanDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateScanDetails([FromQuery] string PalletId, int ScannedQty, string ToStatus, [FromQuery] string OrderNo = null, int VendorId = 0)
        {
            List<string> StrPalletIds = new List<string>();
            StrPalletIds = PalletId.Split(',').ToList();

            List<string> PalletIds = new List<string>();

            foreach (var id in StrPalletIds)
            {
                PalletIds.Add(id);
            }

            await _transactionDetailsProvider.UpdateScanDetails(PalletIds, ScannedQty, ToStatus, OrderNo, VendorId);
            return Ok();
        }

        //[VechicleDetails_Add]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vechicleDetails"></param>
        /// <param name="ToDCStage"></param>
        /// <param name="ToPalletStage"></param>
        /// <returns>List of DC's</returns>
        [SwaggerOperation(
            Description = "Save TransportAndDCStatusDetails.",
            Tags = new[] { "SaveTransportAndDCStatusDetails" },
            OperationId = "SaveTransportAndDCStatusDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SaveTransportAndDCStatusDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> SaveTransportAndDCStatusDetails(VechicleDetails vechicleDetails, [FromQuery] string ToDCStage, [FromQuery] string ToPalletStage)
        {
            await _transactionDetailsProvider.SaveVehicleAndUpdateStatus(vechicleDetails, ToDCStage, ToPalletStage);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>        
        /// <returns>Save Order</returns>
        [SwaggerOperation(
            Description = "Save SaveHHTOrderDetails.",
            Tags = new[] { "SaveHHTOrderDetails" },
            OperationId = "SaveHHTOrderDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SaveHHTOrderDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> SaveHHTOrderDetails(List<OrderDetails> orderDetails)
        {
            await _transactionDetailsProvider.SaveHHTOrderDetails(orderDetails);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>        
        /// <returns>Save Order</returns>
        [SwaggerOperation(
            Description = "UpdateHHTDispatchDetails.",
            Tags = new[] { "UpdateHHTDispatchDetails" },
            OperationId = "UpdateHHTDispatchDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("UpdateHHTDispatchDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateHHTDispatchDetails(List<OrderDetails> orderDetails)
        {
            await _transactionDetailsProvider.UpdateHHTDispatchDetails(orderDetails);
            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PalletId"></param>
        /// <param name="DCStatus"></param>
        /// <param name="DCNo"></param>
        /// <returns>List of DC's</returns>
        [SwaggerOperation(
            Description = "GetDCDetailsByPallet.",
            Tags = new[] { "GetDCDetailsByPallet" },
            OperationId = "GetDCDetailsByPallet")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetDCDetailsByPallet"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDCDetailsByPallet([FromQuery] string PalletId = "", [FromQuery] int DCStatus = 0, [FromQuery] string DCNo = "")
        {
            var response = await _transactionDetailsProvider.GetDCDetailsByPallet(PalletId, DCStatus, DCNo);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PalletPartNo"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "GetPalletPartNo.",
            Tags = new[] { "GetPalletPartNo" },
            OperationId = "GetPalletPartNo")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletPartNo"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPalletPartNo([FromQuery] string PalletPartNo = "")
        {
            var response = await _transactionDetailsProvider.GetPalletPartNo(PalletPartNo);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>        
        /// <returns>Save Order</returns>
        [SwaggerOperation(
            Description = "UpdateHHTDispatchDetails.",
            Tags = new[] { "UpdateHHTDispatchDetails" },
            OperationId = "UpdateHHTDispatchDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("UpdateHHTInwardDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateHHTInwardDetails(List<OrderDetails> orderDetails)
        {
            await _transactionDetailsProvider.UpdateHHTInwardDetails(orderDetails);
            return Ok();
        }

        //GetPalletForPutAway


        /// <summary>
        /// 
        /// </summary>

        /// <returns>List of DC's</returns>
        [SwaggerOperation(
            Description = "GetPalletForPutAway.",
            Tags = new[] { "GetPalletForPutAway" },
            OperationId = "GetPalletForPutAway")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletForPutAway"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPalletForPutAway()
        {
            var response = await _transactionDetailsProvider.GetPalletForPutAway();
            return Ok(response);
        }
        //UpdatePalletWriteCount

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PalletId"></param>        
        /// <returns>Save Order</returns>
        [SwaggerOperation(
            Description = "UpdatePalletWriteCount.",
            Tags = new[] { "UpdatePalletWriteCount" },
            OperationId = "UpdatePalletWriteCount")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("UpdatePalletWriteCount"), MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdatePalletWriteCount([FromQuery] string PalletId)
        {
            await _transactionDetailsProvider.UpdatePalletWriteCount(PalletId);
            return Ok();
        }

    }
}
