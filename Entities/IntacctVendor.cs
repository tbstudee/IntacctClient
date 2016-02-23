using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
    public class IntacctVendor : IntacctObject
    {
        [IntacctName("vendorid")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string TermName { get; set; }
        public IntacctContact PrimaryContact { get; set; }
        public IntacctContact PayToContact { get; set; }

        public IntacctVendor(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public IntacctVendor(XElement data)
        {
            this.SetPropertyValue(x => Id, data);
            this.SetPropertyValue(x => Name, data);
            this.SetPropertyValue(x => TermName, data);

            var primaryContactElement = data.Element("primary");
            if (primaryContactElement != null && primaryContactElement.HasElements)
            {
                PrimaryContact = new IntacctContact(primaryContactElement);
            }

            var payToContactElement = data.Element("payto");
            if (payToContactElement != null && payToContactElement.HasElements)
            {
                PayToContact = new IntacctContact(payToContactElement);
            }

        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>
            {
                new XElement("vendorid", Id),
                new XElement("name", Name),
                new XElement("termname", TermName),
                new XElement("primary", PrimaryContact?.ToXmlElements().Cast<object>()),

            };

            return elements.ToArray();
        }
    }
}
