using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
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
            Description = "Get Order Details.",
            Tags = new[] { "GetOrderDetails" },
            OperationId = "GetOrderDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("GetOrderDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetOrderDetails(OrderDetails orderDetails)
        {
            var response = await _transactionDetailsProvider.GetOrderDetails(orderDetails);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNo"></param>
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
        /// <param name="PalletIds"></param>
        /// <param name="ScannedQty"></param>
        /// <param name="ToStatus"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Update Scan details.",
            Tags = new[] { "UpdateScanDetails" },
            OperationId = "UpdateScanDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("UpdateScanDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateScanDetails(List<int> PalletIds, int ScannedQty, string ToStatus)
        {
            await _transactionDetailsProvider.UpdateScanDetails(PalletIds, ScannedQty, ToStatus);
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

    }
}
