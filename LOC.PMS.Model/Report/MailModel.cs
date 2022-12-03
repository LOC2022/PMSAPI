using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.Report
{
    public class MailModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
