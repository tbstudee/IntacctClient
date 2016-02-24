using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Terms.AR
{
    public enum ARTermStatus
    {
        Inactive,
        Active
    }
    [IntacctName("arterm")]
    public class IntacctARTerm : IntacctObject
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public ARTermStatus Status { get; set; }
        public Terms Terms { get; set; }
        public Discount Discount { get; set; }
        public DiscountCalculatedOn DiscountCalculatedOn { get; set; }
        public IntacctARTerm() { } 

        public IntacctARTerm(XElement data)
        {
            this.SetPropertyValue(x => x.Name, data);
            this.SetPropertyValue(x => x.Description, data);

            var statusElement = data.Element("status");
            if (statusElement != null && !statusElement.IsEmpty)
            {
                switch (statusElement.Value.ToLower())
                {
                    case "active":
                        Status = ARTermStatus.Active;
                        break;
                    case "inactive":
                        Status = ARTermStatus.Inactive;
                        break;
                    default:
                        throw new InvalidDataException($"Unable to read AR Term status.");
                }
            }

            var termsElement = data.Element("due");
            if (termsElement != null && termsElement.HasElements)
            {
                Terms = new Terms(termsElement);
            }

            var discountElement = data.Element("discount");
            if (discountElement != null && discountElement.HasElements)
            {
                Discount = new Discount(discountElement);
            }

            var discountCalculatedOnElement = data.Element("disccalcon");
            if (discountCalculatedOnElement != null && !discountCalculatedOnElement.IsEmpty)
            {
                switch (discountCalculatedOnElement.Value.ToLower())
                {
                    case "invoice total":
                        DiscountCalculatedOn = DiscountCalculatedOn.InvoiceTotalWithAddedCharges;
                        break;
                    case "line items total":
                        DiscountCalculatedOn = DiscountCalculatedOn.LineItemsTotalExcludingAddedCharges;
                        break;
                    default:
                        break;
                }
            }
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("name", Name),
                new XElement("description", Description),
                new XElement("status", Status.ToIntacctOptionString()),
                new XElement("due", Terms.ToXmlElements().Cast<object>())
            };

            if (Discount != null)
            {
                elements.Add(new XElement("discount", Discount.ToXmlElements().Cast<object>()));
                elements.Add(new XElement("disccalcon", DiscountCalculatedOn.ToIntacctOptionString()));
            }

            return elements.ToArray();
        }
    }
}
