using System;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
	public class IntacctLineItem : IntacctObject
	{
	    public IntacctLineItem(XElement data)
	    {
	        this.SetPropertyValue(x => AccountNumber, data);
	        this.SetPropertyValue(x => AccountLabel, data, true);
	        this.SetPropertyValue(x => Amount, data);
	        this.SetPropertyValue(x => Memo, data);
	        this.SetPropertyValue(x => LocationId, data);
	        this.SetPropertyValue(x => DepartmentId, data);
	    }

        [IntacctName("glaccountno")]
		public string AccountNumber { get; private set; }
        [IntacctName("accountlabel")]
		public string AccountLabel { get; private set; }
        
		public decimal Amount { get; set; }

		public string Memo { get; set; }
        public string LocationId { get; set; }
        public string DepartmentId { get; set; }

		public static IntacctLineItem CreateWithAccountNumber(string accountNumber, decimal amount)
		{
			return new IntacctLineItem(amount) { AccountNumber = accountNumber };
		}

		public static IntacctLineItem CreateWithAccountLabel(string accountLabel, decimal amount)
		{
			return new IntacctLineItem(amount) { AccountLabel = accountLabel };
		}

        
		private IntacctLineItem(decimal amount)
		{
			Amount = amount;
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
				       {
					       GetAccountXmlElement(),
					       new XElement("amount", Amount),
						   new XElement("memo", Memo ?? ""), 
                           new XElement("locationid", LocationId),
                           new XElement("departmentid", DepartmentId)
				       };
		}

		private XElement GetAccountXmlElement()
		{
			if (!string.IsNullOrWhiteSpace(AccountNumber)) return new XElement("glaccountno", AccountNumber);
			if (!string.IsNullOrWhiteSpace(AccountLabel)) return new XElement("accountlabel", AccountLabel);

			throw new Exception("Unable to generate line item XML because neither account number or label are set.");
		}
	}
}
