﻿using System;
using System.Reflection;
using System.Xml.Linq;
using Intacct.Entities;
using Intacct.Infrastructure;

namespace Intacct.Operations
{
	public class GetEntityOperation<TEntity> : IntacctAuthenticatedOperationBase<TEntity> where TEntity : IntacctObject
	{
		private readonly string _entityName;
		private readonly string _entityId;
		private readonly bool _isExtenalKey;

		public GetEntityOperation(IIntacctSession session, string entityId, bool isExtenalKey = false) : base(session, "get", "data", mayHaveEmptyResult: true)
		{
			if (entityId == null) throw new ArgumentNullException(nameof(entityId));
			if (string.IsNullOrWhiteSpace(entityId)) throw new ArgumentException($"Argument {nameof(entityId)} may not be empty.", nameof(entityId));

			_entityName = LambdaExtensions.GetObjectName<TEntity>();
			_entityId = entityId;
			_isExtenalKey = isExtenalKey;
		}

		protected override XObject[] CreateFunctionContents()
		{
			return new XObject[]
				       {
					       new XAttribute("object", _entityName),
					       new XAttribute("key", _entityId),
					       new XAttribute("externalkey", _isExtenalKey.ToString().ToLowerInvariant()),
				       };
		}

		protected override IntacctOperationResult<TEntity> ProcessResponseData(XElement responseData)
		{
			var entityElement = responseData.Element(_entityName);

			var entity = (TEntity) Activator.CreateInstance(typeof(TEntity), entityElement);

			return new IntacctOperationResult<TEntity>(entity);
		}


	}
}
