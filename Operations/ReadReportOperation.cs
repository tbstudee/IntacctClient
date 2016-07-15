using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
    public class ReadReportOperation : IntacctAuthenticatedOperationBase<IntacctReadReport>
    {
        private readonly string _reportName;

        public ReadReportOperation(IIntacctSession session, string reportName, int pageSize = 100, int waitTime=1) : base(session, "readReport", "data", mayHaveEmptyResult: true)
        {
            if (reportName == null) throw new ArgumentNullException(nameof(reportName));
            if (string.IsNullOrWhiteSpace(reportName)) throw new ArgumentException($"Argument {nameof(reportName)} may not be empty.", nameof(reportName));

            _reportName = reportName;
        }
        public override XElement GetOperationElement()
        {
            return new XElement("operation",
                    CreateAuthElement(),
                    new XElement("content",
                                 new XElement("function",
                                              new XAttribute("controlid", Id),
                                              new XElement(FunctionName,
                                                    new XAttribute("returnDef", false),
                                                           CreateFunctionContents()?.Cast<object>()))));
        }

        protected override XObject[] CreateFunctionContents()
        {
            return new XObject[]
            {
                new XElement("report", _reportName)
            };
        }

        protected override IntacctOperationResult<IntacctReadReport> ProcessResponseData(XElement responseData)
        {
            var results = responseData.Descendants("report_results").Single();
            var reportId = results.Elements("REPORTID").First().Value;
            var status = results.Elements("STATUS").First().Value;

            var readReport = new IntacctReadReport(reportId, status);

            return new IntacctOperationResult<IntacctReadReport>(readReport);
        }
    }
}
