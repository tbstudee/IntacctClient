using System.Collections;
using System.ServiceModel.Description;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public abstract class IntacctObject
	{
		internal abstract XObject[] ToXmlElements();
	}
}
