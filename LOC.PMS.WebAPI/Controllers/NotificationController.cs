using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// Notification Controller.
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationpProvider _notificationProvider;

        /// <summary>
        /// Notification Controller constructor.
        /// </summary>
        /// <param name="notificationProvider"></param>
        public NotificationController(INotificationpProvider notificationProvider)
        {
            this._notificationProvider = notificationProvider;
        }

        /// <summary>
        /// Send SMS notification for the specified recepients.
        /// </summary>
        /// <param name="smsNotificationRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "SMS notification for the specified recepients.",
            Tags = new[] { "SMSNotification" },
            OperationId = "SMSNotification")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("SMSNotification"), MapToApiVersion("1.0")]
        public async Task<IActionResult> SMSNotification([FromBody] SmsNotification smsNotificationRequest)
        {
          await _notificationProvider.SMSNotification(smsNotificationRequest);
          return Ok();
        }

        /// <summary>
        /// Send Email Notification for the specified recepients.
        /// </summary>
        /// <param name="emailNotificationRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Send Email Notification for the specified recepients.",
            Tags = new[] { "EmailNotification" },
            OperationId = "EmailNotification")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("EmailNotification"), MapToApiVersion("1.0")]
        public async Task<IActionResult> EmailNotification([FromBody] EmailNotification emailNotificationRequest)
        {
            await _notificationProvider.EmailNotification(emailNotificationRequest);
            return Ok();
        }
    }
}