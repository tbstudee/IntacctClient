using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Intacct.Entities.Terms.AR;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
    [IntacctName("invoice")]
	public class IntacctInvoice : IntacctObject
	{
        [IntacctName("customerid")]
		public string CustomerId { get; set; }

		public IntacctDate DateCreated { get; set; }

		public IntacctDate DateDue { get; set; }
        
        public string InvoiceNo { get; set; }

        public string ShipTo { get; set; }

		public ICollection<IntacctLineItem> Items { get; set; }

        public string TermName { get; set; }

	    public IntacctInvoice(string customerId, IntacctDate dateCreated) : this(customerId, dateCreated, null) { }
		public IntacctInvoice(string customerId, IntacctDate dateCreated, IntacctDate dateDue)
		{
			CustomerId = customerId;
			DateCreated = dateCreated;
			DateDue = dateDue;
			Items = new List<IntacctLineItem>();
		}

        public IntacctInvoice(XElement data)
        {
            this.SetPropertyValue(x => CustomerId, data);
            this.SetPropertyValue(x => DateCreated, data);
            this.SetPropertyValue(x => DateDue, data);
            this.SetPropertyValue(x => TermName, data);
            this.SetPropertyValue(x => InvoiceNo, data);
            
            var itemElements = data.Elements("invoiceitems").Descendants().ToList();
            if (itemElements.Any())
            {
                Items = new List<IntacctLineItem>();

                foreach (var item in itemElements)
                {
                    Items.Add(new IntacctLineItem(item));
                }
            }
        }
        
		public void AddItem(IntacctLineItem item)
		{
			Items.Add(item);
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
				       {
					       new XElement("customerid", CustomerId),
					       new XElement("datecreated", DateCreated.ToXmlElements().Cast<object>()),
                           new XElement("datedue", DateDue.ToXmlElements().Cast<object>()),
                           new XElement("termname", TermName),
                           new XElement("invoiceno", InvoiceNo),
                           new XElement("shipto", new object[] { new XElement("contactname", ShipTo) }),
					       new XElement("invoiceitems", Items.Select(item => new XElement("lineitem", item.ToXmlElements().Cast<object>()))),
				       };
		}
	}

    [IntacctName("arinvoice")]
    public class InvoiceRecordNoQuery : IntacctObject
    {
        public InvoiceRecordNoQuery(XElement data)
        {
            this.SetPropertyValue(x => RecordNo, data);
        }

        [IntacctName("RECORDNO")]
        public string RecordNo { get; set; }
        internal override XObject[] ToXmlElements()
        {
            return new XObject[]
            {
                new XElement("recordno", RecordNo)
            };
        }
    }
}
