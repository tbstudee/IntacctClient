using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Terms
{
    public enum DiscountAmountUnit
    {
        Percent,
        LumpSum
    }
    public class Discount : IntacctObject
    {
        public string DaysForward { get; set; }
        public string DiscountFrom { get; set; }
        [IntacctName("amount")]
        public string DiscountAmount { get; set; }
        public DueFrom DiscountDueFrom { get; set; }
        public DiscountAmountUnit DiscountAmountUnit { get; set; }

        [IntacctName("gracedays")]
        public string GraceDays { get; set; }

        public Discount(string daysForward, DueFrom dueFrom, string percentAmount, DiscountAmountUnit discountAmountUnit, string graceDays)
        {
            DaysForward = daysForward;
            DiscountAmount = percentAmount;
            GraceDays = graceDays;
            DiscountAmountUnit = discountAmountUnit;
            DiscountDueFrom = dueFrom;
        }

        public Discount(XElement data)
        {
            this.SetPropertyValue(d => d.DaysForward, data, true);
            this.SetPropertyValue(d => d.DiscountAmount, data, true);
            this.SetPropertyValue(d => d.GraceDays, data, true);
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("discday", DaysForward),
                new XElement("discfrom", DiscountFrom),
                new XElement("discamount", DiscountAmount),
                new XElement("discgracedays", GraceDays),
            };

            return elements.ToArray();
        }
    }
}
