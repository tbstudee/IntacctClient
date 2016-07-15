using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
    public class IntacctReadReport
    {
        public string ReportId { get; set; }
        public string Status { get; set; }

        public IntacctReadReport(string reportId, string status)
        {
            ReportId = reportId;
            Status = status;
        }
    }
}
