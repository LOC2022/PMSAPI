using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOC.PMS.Model
{
    public class SmsNotification
    {
        public string FromPhoneNumber { get; set; }

        public List<string> ToPhoneNumber { get; set; }

        public string Message {get; set;}

    }
    
    public class EmailNotification
    {
        public string FromEmailAddress { get; set; }

        public  List<string> ToEmailAddress { get; set; }

        public string Subject { get; set; }

        public string Content {get; set;}

        public ContentType contentType {get; set;}

    }

    public enum ContentType
    {
        PlainText,

        HttmlContent
    }
}