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
        [HttpGet("GetOrderDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] OrderDetails orderDetails)
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
        public async Task<IActionResult> GetDCDetails([FromQuery] string OrderNo = GETALL)
        {
            var response = await _transactionDetailsProvider.GetDCDetails(OrderNo);
            return Ok(response);
        }

    }
}
