using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
    public class CreateContactOperation : IntacctAuthenticatedOperationBase<String>
    {
        private readonly IntacctContact _contact;
        public CreateContactOperation(IIntacctSession session, IntacctContact contact)
            : base(session, "create_contact", "key")
        {
            _contact = contact;
        }

        protected override XObject[] CreateFunctionContents()
        {
            var elements = _contact.ToXmlElements();

            return elements;
        }

        protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
        {
            return new IntacctOperationResult<string>(responseData.Value);
        }
    }
}
