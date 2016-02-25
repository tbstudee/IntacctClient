using System.Xml.Linq;
using Intacct.Entities.Terms.AP;

namespace Intacct.Operations
{
    public class CreateAPTermOperation : IntacctAuthenticatedOperationBase<string>
    {
        private readonly IntacctAPTerm _term;

        public CreateAPTermOperation(IIntacctSession session, IntacctAPTerm term) : base(session, "create_apterm", "key", true)
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