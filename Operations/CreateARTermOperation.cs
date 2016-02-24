using System.Xml.Linq;
using Intacct.Entities.Terms.AR;

namespace Intacct.Operations
{
    public class CreateARTermOperation : IntacctAuthenticatedOperationBase<string>
    {
        private readonly IntacctARTerm _term;

        public CreateARTermOperation(IIntacctSession session, IntacctARTerm term) : base(session, "create_arterm", "key", true)
        {
            _term = term;
        }

        protected override XObject[] CreateFunctionContents()
        {
            var elements = _term.ToXmlElements();
            return elements;
        }

        protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
        {
            return new IntacctOperationResult<string>(responseData.Value);
        }
    }
}