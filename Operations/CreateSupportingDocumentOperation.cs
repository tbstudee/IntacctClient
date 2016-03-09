using System.Xml.Linq;
using Intacct.Entities.Supporting_Documents;

namespace Intacct.Operations
{
    public class CreateSupportingDocumentOperation : IntacctAuthenticatedOperationBase<string>
    {
        private readonly IntacctSupportingDocument _document;
        public CreateSupportingDocumentOperation(IIntacctSession session,  IntacctSupportingDocument document) : base(session, "create_supdoc", "key", true)
        {
            _document = document;
        }

        protected override XObject[] CreateFunctionContents()
        {
            var elements = _document.ToXmlElements();
            return elements;
        }

        protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
        {
            return new IntacctOperationResult<string>(responseData.Value);
        }
    }
}