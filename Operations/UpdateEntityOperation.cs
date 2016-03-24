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
        private readonly TEntity _entity;

        public UpdateEntityOperation(IIntacctSession session, TEntity entity) : base(session, "update", "data", mayHaveEmptyResult: false)
        {
            _entity = entity;
        }

        public override XElement GetOperationElement()
        {
            return new XElement("operation",
                    CreateAuthElement(),
                    new XElement("content",
                                 new XElement("function",
                                              new XAttribute("controlid", Id),
                                              new XElement(FunctionName,
                                                    new XElement(GetObjectName(),
                                                           CreateFunctionContents()?.Cast<object>())))));
        }

        protected override XObject[] CreateFunctionContents()
        {
            return _entity.ToXmlElements();
        }

        protected override IntacctOperationResult<TEntity> ProcessResponseData(XElement responseData)
        {
            return new IntacctOperationResult<TEntity>(_entity);
        }

        private string GetObjectName()
        {
            var attribute = typeof(TEntity).GetTypeInfo().GetCustomAttribute<IntacctNameAttribute>();
            if (attribute == null)
            {
                throw new Exception($"Unable to create \"update\" request for entity of type {typeof(TEntity).Name} because it is missing the {nameof(IntacctNameAttribute)} attribute.");
            }

            return attribute.Name;
        }
    }
}
