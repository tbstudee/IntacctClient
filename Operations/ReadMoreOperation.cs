using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
    public class ReadMoreOperation<T> : IntacctAuthenticatedOperationBase<T> 
    {
        private readonly string _reportId;
        public ReadMoreOperation(IIntacctSession session, string reportId) : base(session, "readMore", "data", mayHaveEmptyResult: false)
        {
            if (reportId == null) throw new ArgumentNullException(nameof(reportId));
            if (string.IsNullOrWhiteSpace(reportId)) throw new ArgumentException($"Argument {nameof(reportId)} may not be empty.", nameof(reportId));

            _reportId = reportId;
        }

        public override XElement GetOperationElement()
        {
            return new XElement("operation",
                CreateAuthElement(),
                new XElement("content",
                     new XElement("function",
                                  new XAttribute("controlid", Id),
                                  new XElement(FunctionName,
                                               CreateFunctionContents()?.Cast<object>()))));
        }

        protected override XObject[] CreateFunctionContents()
        {
            return new XObject[]
            {
                new XElement("reportId", _reportId)
            };
        }

        public IEnumerable<T> Results { get; set; }

        protected override IntacctOperationResult<T> ProcessResponseData(XElement responseData)
        {
            IntacctOperationResult<T> opResult = null;

            var result = responseData.Descendants("data").Single();
            var status = result.Elements("status").FirstOrDefault();
            
            if (status != null)
            {
                opResult =
                    (IntacctOperationResult<T>) Activator.CreateInstance(typeof(IntacctOperationResult<T>), status);
            }
            else
            {
                var items = result.Elements("data").ToList();
                foreach (var item in items)
                {
                    
                }
            }

            return opResult;
        }
    }
}
