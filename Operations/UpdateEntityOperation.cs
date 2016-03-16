using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
    public class UpdateEntityOperation<TEntity> : IntacctAuthenticatedOperationBase<TEntity> where TEntity : IntacctObject
    {
        public UpdateEntityOperation(IIntacctSession session, string functionName, string responseElementName, bool mayHaveEmptyResult = false) : base(session, functionName, responseElementName, mayHaveEmptyResult)
        {
        }

        protected override XObject[] CreateFunctionContents()
        {
            throw new NotImplementedException();
        }

        protected override IntacctOperationResult<TEntity> ProcessResponseData(XElement responseData)
        {
            throw new NotImplementedException();
        }
    }
}
