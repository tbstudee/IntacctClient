using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;
using Intacct.Infrastructure;

namespace Intacct.Operations
{
    public class UpdateEntityOperation<TEntity> : IntacctAuthenticatedOperationBase<TEntity> where TEntity : IntacctObject
    {
        private readonly string _entityName;
        private readonly string _entityId;
        private readonly bool _isExternalKey;

        public UpdateEntityOperation(IIntacctSession session, string entityId, string responseElementName, bool isExternalKey = false) : base(session, "update", responseElementName, mayHaveEmptyResult: true)
        {
            if (entityId == null) throw new ArgumentNullException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityId)) throw new ArgumentException($"Argument {nameof(entityId)} may not be empty.", nameof(entityId));

            _entityName = GetObjectName<TEntity>();
            _entityId = entityId;
            _isExternalKey = isExternalKey;
        }

        protected override XObject[] CreateFunctionContents()
        {
            throw new NotImplementedException();
        }

        protected override IntacctOperationResult<TEntity> ProcessResponseData(XElement responseData)
        {
            var entityElement = responseData.Element(_entityName);

            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity), entityElement);

            return new IntacctOperationResult<TEntity>(entity);
        }

        private string GetObjectName<T>()
        {
            var attribute = typeof(T).GetTypeInfo().GetCustomAttribute<IntacctNameAttribute>();
            if (attribute == null)
            {
                throw new Exception($"Unable to create \"update\" request for entity of type {typeof(T).Name} because it is missing the {nameof(IntacctNameAttribute)} attribute.");
            }

            return attribute.Name;
        }
    }
}
