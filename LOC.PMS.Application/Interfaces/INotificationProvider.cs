using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface INotificationpProvider
    {
        Task SMSNotification(SmsNotification smsNotificationRequest);

        Task EmailNotification(EmailNotification emailNotificationRequest);
    }
}