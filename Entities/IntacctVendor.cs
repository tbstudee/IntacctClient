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
        public string GLAccountNo { get; set; }
        public string Status { get; set; }
        public string ExternalId { get; set; }
        public IntacctContact PayToContact { get; set; }
        public IntacctContact ContactInfo { get; set; }

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
            this.SetPropertyValue(x => ExternalId, data);

            var payToContactElement = data.Element("payto");
            if (payToContactElement != null && payToContactElement.HasElements)
            {
                PayToContact = new IntacctContact(payToContactElement);
            }

            var contactInfoElement = data.Element("contactinfo");
            if (contactInfoElement != null && contactInfoElement.HasElements)
            {
                ContactInfo = new IntacctContact(contactInfoElement);
            }

        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>
            {
                new XElement("vendorid", Id),
                new XElement("name", Name),
                new XElement("termname", TermName),
                new XElement("glaccountno", GLAccountNo),
                new XElement("status", Status),
                new XElement("externalid", ExternalId)
            };

            if (PayToContact != null)
            {
                elements.Add(new XElement("payto", new XElement("contact",
                    PayToContact.ToXmlElements().Cast<object>())));
            }

            if (ContactInfo != null)
            {
                elements.Add(new XElement("contactinfo", new XElement("contact",
                    ContactInfo.ToXmlElements().Cast<object>())));
            }

            return elements.ToArray();
        }
    }
}
