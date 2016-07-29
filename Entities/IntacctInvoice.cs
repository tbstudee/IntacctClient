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
		public string CustomerId { get; }

		public IntacctDate DateCreated { get; }
		public IntacctDate DateDue { get; }
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
}
