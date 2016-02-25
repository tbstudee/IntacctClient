using System.IO;
using Intacct.Entities;
using Intacct.Entities.Terms;
using Intacct.Entities.Terms.AP;
using Intacct.Entities.Terms.AR;

namespace Intacct.Infrastructure
{
    public static class EnumExtensions
    {
        public static string ToIntacctOptionString(this DiscountAmountUnit disc)
        {
            switch (disc)
            {
                case DiscountAmountUnit.LumpSum:
                    return "$";
                case DiscountAmountUnit.Percent:
                    return "%";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from DiscountAmountUnit value");
            }
        }
        public static string ToIntacctOptionString(this TermStatus status)
        {
            switch (status)
            {
                case TermStatus.Active:
                    return "active";
                case TermStatus.Inactive:
                    return "inactive";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from TermStatus value");
            }
        }

        public static string ToIntacctOptionString(this ARDiscountCalculatedOn calcOn)
        {
            switch (calcOn)
            {
                case ARDiscountCalculatedOn.InvoiceTotalWithAddedCharges:
                    return "Invoice total";
                case ARDiscountCalculatedOn.LineItemsTotalExcludingAddedCharges:
                    return "Line items total";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from ARDiscountCalculatedOn value");
            }
        }

        public static string ToIntacctOptionString(this APDiscountCalculatedOn calcOn)
        {
            switch (calcOn)
            {
                case APDiscountCalculatedOn.BillTotalIncludingAllCharges:
                    return "Bill total";
                case APDiscountCalculatedOn.LineItemsTotalExcludingAddedCharges:
                    return "Line items total";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from APDiscountCalculatedOn value");
            }
        }

        public static string ToIntacctOptionString(this DueFrom dueFrom)
        {
            switch (dueFrom)
            {
                case DueFrom.InvoiceDate:
                    return "from invoice/bill date";
                case DueFrom.OfMonth:
                    return "of the month of invoice/bill date";
                case DueFrom.OfNextMonth:
                    return "of next month from invoice/bill date";
                case DueFrom.OfSecondMonth:
                    return "of 2nd month from invoice/bill date";
                case DueFrom.OfThirdMonth:
                    return "of 3rd month from invoice/bill date";
                case DueFrom.OfFourthMonth:
                    return "of 4th month from invoice/bill date";
                case DueFrom.OfFifthMonth:
                    return "of 5th month from invoice/bill date";
                case DueFrom.OfSixthMonth:
                    return "of 6th month from invoice/bill date";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from DueFrom value");
            }
        }
    }
}