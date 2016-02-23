using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
    public class CreateVendorOperation : IntacctAuthenticatedOperationBase<string>
    {
        private readonly IntacctVendor _vendor;

        public CreateVendorOperation(IIntacctSession session, IntacctVendor vendor)
            : base(session, "create_vendor", "key")
        {
            _vendor = vendor;
        }

        protected override XObject[] CreateFunctionContents()
        {
            var elements = _vendor.ToXmlElements();

            return elements;
        }

        protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
        {
            return new IntacctOperationResult<string>(responseData.Value);
        }
    }
}
