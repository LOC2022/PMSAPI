using LOC.PMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// Transaction Details Controller.
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ITransactionDetailsProvider _transactionDetailsProvider;

        /// <summary>
        /// Pallet Details Controller constructor.
        /// </summary>
        /// <param name="transactionDetailsProvider"></param>
        public TransactionDetailsController(ITransactionDetailsProvider transactionDetailsProvider)
        {
            this._transactionDetailsProvider = transactionDetailsProvider;
        }

    }
}
