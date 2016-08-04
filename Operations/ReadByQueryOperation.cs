using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;
using Intacct.Infrastructure;

namespace Intacct.Operations
{
    public class ReadByQueryOperation<TEntity> : IntacctAuthenticatedOperationBase<TEntity> where TEntity : IntacctObject
    {
        // TODO: IntacctQueryBuilder instead of plain string(s)
        private readonly string _query;
        private readonly string _fields;

        private readonly string _entityName;

        public ReadByQueryOperation(IIntacctSession session, string query, string fields = "*", bool mayHaveEmptyResult = true) : base(session, "readByQuery", "data", mayHaveEmptyResult)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException($"Argument {nameof(query)} may not be empty.", nameof(query));
            
            _query = query;
            _fields = fields;

            _entityName = LambdaExtensions.GetObjectName<TEntity>();
        }

        protected override XObject[] CreateFunctionContents()
        {
            return new XObject[]
            {
                new XElement("object", _entityName),
                new XElement("fields", _fields),
                new XElement("query", _query)
            };
        }

        protected override IntacctOperationResult<TEntity> ProcessResponseData(XElement responseData)
        {
            
            var entityElement = responseData.Element(_entityName);
            if (entityElement.NodesAfterSelf().Any())
            {
                throw new InvalidOperationException(
                    "Queries which return more than a single result are not yet supported!");
            }

            var entity = (TEntity) Activator.CreateInstance(typeof(TEntity), entityElement);

            return new IntacctOperationResult<TEntity>(entity);
        }
    }
}
