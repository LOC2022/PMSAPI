using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace LOC.PMS.Application
{
    public class NotificationProvider : INotificationpProvider
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public NotificationProvider(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task SMSNotification(SmsNotification smsNotificationRequest)
        {
            try
            {
                _logger.ForContext("smsNotificationRequest", smsNotificationRequest)
                   .Information("SMS Notification - Start");

                await SMSNotificationProcess(smsNotificationRequest);

                _logger.ForContext("smsNotificationRequest", smsNotificationRequest)
                   .Information("SMS Notification - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("smsNotificationRequest", smsNotificationRequest)
                    .Error(exception, "Exception occurred while sending SMS Notification.");
                await Task.FromException(exception);
            }
        }

        public async Task<string> EmailNotification(EmailNotification emailNotificationRequest)
        {
            try
            {
                _logger.ForContext("emailNotificationRequest", emailNotificationRequest)
                   .Information("Email Notificaiton= - Start");

                await EmailNotificationProcess(emailNotificationRequest);

                return "Success" + Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

                _logger.ForContext("emailNotificationRequest", emailNotificationRequest)
                        .Information("Email Notificaiton= - End");
            }
            catch (Exception exception)
            {
                return exception.Message + Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                _logger.ForContext("emailNotificationRequest", emailNotificationRequest)
                    .Error(exception, "Exception occurred while sending Email Notificaiton");
                await Task.FromException(exception);

            }
        }

        private async Task EmailNotificationProcess(EmailNotification emailNotificationRequest)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            string fromEmail = emailNotificationRequest.FromEmailAddress ?? _config.GetValue<string>("NotificationSettings:EmailNotification:FromEmail");

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "Ace Digital"),
                Subject = emailNotificationRequest.Subject,
                HtmlContent = emailNotificationRequest.Content
            };

            var toEmailList = new List<EmailAddress>();
            if (emailNotificationRequest.ToEmailAddress.Count > 0)
            {
                foreach (var toEmail in emailNotificationRequest.ToEmailAddress)
                {
                    toEmailList.Add(new EmailAddress(toEmail));
                }
            }

            var message = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, toEmailList, msg.Subject, "", msg.HtmlContent);

            var result = await client.SendEmailAsync(message);
            if (result.IsSuccessStatusCode)
            {
                _logger.ForContext("emailNotificationRequest", emailNotificationRequest)
                   .Information($"Email Notificaiton for {toEmailList} has been sent successfully with satus code {result.StatusCode}");
            }
        }

        private Task SMSNotificationProcess(SmsNotification smsNotificationRequest)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            string fromPhoneNumber = smsNotificationRequest.FromPhoneNumber ?? _config.GetValue<string>("NotificationSettings:SMSNotification:FromNumber");

            TwilioClient.Init(accountSid, authToken);

            foreach (var toPhoneNumber in smsNotificationRequest.ToPhoneNumber)
            {
                var message = MessageResource.Create(
                    body: smsNotificationRequest.Message,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );

                _logger.ForContext("smsNotificationRequest", smsNotificationRequest)
                    .Information($"SMS Notification for {toPhoneNumber} has been {message.Status}");
            }

            return Task.CompletedTask;
        }
    }
}