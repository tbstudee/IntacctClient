using System.IO;
using Intacct.Entities.Terms;
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
        public static string ToIntacctOptionString(this ARTermStatus status)
        {
            switch (status)
            {
                case ARTermStatus.Active:
                    return "active";
                case ARTermStatus.Inactive:
                    return "inactive";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from ARTermStatus value");
            }
        }

        public static string ToIntacctOptionString(this DiscountCalculatedOn calcOn)
        {
            switch (calcOn)
            {
                case DiscountCalculatedOn.InvoiceTotalWithAddedCharges:
                    return "invoice total";
                case DiscountCalculatedOn.LineItemsTotalExcludingAddedCharges:
                    return "line items total";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from DiscountCalculatedOn value");
            }
        }

        public static string ToIntacctOptionString(this TermsDueFrom dueFrom)
        {
            switch (dueFrom)
            {
                case TermsDueFrom.InvoiceDate:
                    return "from invoice/bill date";
                case TermsDueFrom.OfMonth:
                    return "of the month of invoice/bill date";
                case TermsDueFrom.OfNextMonth:
                    return "of next month from invoice/bill date";
                case TermsDueFrom.OfSecondMonth:
                    return "of 2nd month from invoice/bill date";
                case TermsDueFrom.OfThirdMonth:
                    return "of 3rd month from invoice/bill date";
                case TermsDueFrom.OfFourthMonth:
                    return "of 4th month from invoice/bill date";
                case TermsDueFrom.OfFifthMonth:
                    return "of 5th month from invoice/bill date";
                case TermsDueFrom.OfSixthMonth:
                    return "of 6th month from invoice/bill date";
                default:
                    throw new InvalidDataException($"Unable to get Intacct option string from TermsDueFrom value");
            }
        }
    }
}