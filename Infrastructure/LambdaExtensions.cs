using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Infrastructure
{
	internal static class LambdaExtensions
	{
		public static void SetPropertyValue<TObj, TProp>(this TObj target, Expression<Func<TObj, TProp>> memberLamda, XElement data, bool isOptional = false)
		{
			var memberSelectorExpression = memberLamda.Body as MemberExpression;
			var property = memberSelectorExpression?.Member as PropertyInfo;

			if (property == null) throw new ArgumentException("Lambda expression is not a valid property member express. Expected syntax is x => x.Prop.", nameof(memberLamda));

			// get Intacct field name
			var fieldName = property.GetCustomAttribute<IntacctNameAttribute>()?.Name ?? property.Name.ToLowerInvariant();

			// get corresponding element
			var element = data.Element(fieldName);
			if (element == null)
			{
				if (isOptional) return;
				throw new ArgumentException($"Intacct data XML element does not contain child element \"{fieldName}\".");
			}

			SetValue(property, target, element);
		}

		private static void SetValue(PropertyInfo property, object target, XElement data)
		{
			if (property.PropertyType == typeof(string))
			{
				property.SetValue(target, data.Value, null);
				return;
			}

			if (property.PropertyType == typeof(int))
			{
			    int intVal = 0;
                if(int.TryParse(data.Value, out intVal))
			    {
			        property.SetValue(target, intVal, null);
			    }
			    return;
			}

		    if (property.PropertyType == typeof(decimal))
		    {
		        decimal decVal = 0.0M;
		        if (decimal.TryParse(data.Value, out decVal))
		        {
		            property.SetValue(target, decVal, null);
		        }
		        return;
		    }

		    if (property.PropertyType == typeof(DateTime))
		    {
		        DateTime dateVal;
		        if (DateTime.TryParse(data.Value, out dateVal))
		        {
		            property.SetValue(target, dateVal, null);
		        }
		        return;
		    }

		    if (property.PropertyType == typeof(IntacctDate))
		    {
		        DateTime date;
		        if (DateTime.TryParse(data.Value, out date))
		        {
		            property.SetValue(target, new IntacctDate(date), null);
		        }
		        return;
		    }

			throw new ArgumentException($"Property type {property.PropertyType.Name} is not yet supported.");
		}

        public static string GetObjectName<T>()
        {
            var attribute = typeof(T).GetTypeInfo().GetCustomAttribute<IntacctNameAttribute>();
            if (attribute == null)
            {
                throw new Exception($"Unable to create \"get\" request for entity of type {typeof(T).Name} because it is missing the {nameof(IntacctNameAttribute)} attribute.");
            }

            return attribute.Name;
        }
    }
}
