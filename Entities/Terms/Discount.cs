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
        [IntacctName("amount")]
        public string DiscountAmount { get; set; }
        public DueFrom DiscountDueFrom { get; set; }
        public DiscountAmountUnit DiscountAmountUnit { get; set; }

        [IntacctName("gracedays")]
        public string GraceDays { get; set; }

        public Discount(string daysForward, DueFrom dueFrom, string discountAmount, DiscountAmountUnit discountAmountUnit, string graceDays)
        {
            DaysForward = daysForward;
            DiscountAmount = discountAmount;
            GraceDays = graceDays;
            DiscountAmountUnit = discountAmountUnit;
            DiscountDueFrom = dueFrom;
        }

        public Discount(XElement data)
        {
            //TODO: fix this comes back differently <percentamount>
            this.SetPropertyValue(d => d.DiscountAmount, data, true);
            this.SetPropertyValue(d => d.GraceDays, data, true);

            var daysForward = data.Element("daysforward");
            //if daysForward element exists then we know its discount days from invoice data
            if (daysForward != null)
            {
                DaysForward = daysForward.Value;
                DiscountDueFrom = DueFrom.InvoiceDate;
            }
            else
            {
                var dayOfMonth = data.Element("dayofmonth");
                if (dayOfMonth != null)
                {
                    DaysForward = dayOfMonth.Value;
                }
                var monthsForward = data.Element("monthsforward");
                if (monthsForward != null)
                {
                    switch (monthsForward.Value.ToLower())
                    {
                        case "0":
                            DiscountDueFrom = DueFrom.OfMonth;
                            break;
                        case "1":
                            DiscountDueFrom = DueFrom.OfNextMonth;
                            break;
                        case "2":
                            DiscountDueFrom = DueFrom.OfSecondMonth;
                            break;
                        case "3":
                            DiscountDueFrom = DueFrom.OfThirdMonth;
                            break;
                        case "4":
                            DiscountDueFrom = DueFrom.OfFourthMonth;
                            break;
                        case "5":
                            DiscountDueFrom = DueFrom.OfFifthMonth;
                            break;
                        case "6":
                            DiscountDueFrom = DueFrom.OfSixthMonth;
                            break;
                    }
                }
            }
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("discday", DaysForward),
                new XElement("discfrom", DiscountDueFrom.ToIntacctOptionString()),
                new XElement("discamount", DiscountAmount),
                new XElement("discpercamn", DiscountAmountUnit.ToIntacctOptionString()),
                new XElement("discgracedays", GraceDays),
            };

            return elements.ToArray();
        }
    }
}
