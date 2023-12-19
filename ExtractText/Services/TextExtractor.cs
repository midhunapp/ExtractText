using ExtractText.Models;
using System.Text.RegularExpressions;

namespace ExtractText.Services
{
    public class TextExtractor : ITextExtractor
    {
        private const decimal TaxRate = 0.1m;// 10%
        public ExtractedResult ExtractText(string text)
        {
            ExtractedResult result = new ExtractedResult();

            var extractedFields = ExtractFields(text);
            if (extractedFields != null)
            {
                if (!extractedFields.Any())
                {
                    return null;
                }

                if (IsUnclosedTagExists(text))
                {
                    return null;
                }
                if (extractedFields.TryGetValue("total", out string totalValue))
                {

                    // var costCentre= 
                    if (decimal.TryParse(totalValue, out decimal total))
                    {
                        var totalExcludingTax = total / (1 + TaxRate);
                        var tax = total - totalExcludingTax;
                        ExtractedData dt = new ExtractedData();
                        dt.Tax = tax;
                        dt.Total = total;
                        dt.TotalExcludingTax = totalExcludingTax;
                        dt.CostCentre = extractedFields.TryGetValue("cost_centre", out string costCentre) ? costCentre : "UNKNOWN";
                        dt.PaymentMethod= extractedFields.TryGetValue("payment_method", out string paymentmethod) ? paymentmethod : "";
                        dt.Vendor = extractedFields.TryGetValue("vendor", out string vendor) ? vendor : "";
                        dt.Description = extractedFields.TryGetValue("description", out string description) ? description : "";
                        dt.Date = extractedFields.TryGetValue("date", out string dateVal) ?dateVal: null;
                        result.ExtractedData = dt;
                        return result;
                    }
                    else
                    {
                        result.IsError = true;
                        result.ErrorMessage = "Invalid value for total";
                        return result;
                    }
                }
                else
                {
                    result.IsError = true;
                    result.ErrorMessage = "Total is missing";
                    return result;
                }
            }

            return null;
        }
        private Dictionary<string, string> ExtractFields(string text)
        {
            var xmlNodes = new Dictionary<string, string>();
            var regex = new Regex("<([^<>]+)[>]([^<>]+)</\\1>");
            var matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                var fieldName = match.Groups[1].Value;
                var fieldValue = match.Groups[2].Value;

                xmlNodes.Add(fieldName, fieldValue);
            }
            return xmlNodes;

        }
        private bool IsUnclosedTagExists(string text)
        {
            //--regular expression for nodes without a closing tag
            var regex = new Regex("<([^<>/]+)[>](?!(.*</\\1>))");
            var matches = regex.Matches(text);
            return matches.Any();
        }
    }
}
