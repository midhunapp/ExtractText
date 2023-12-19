namespace ExtractText.Models
{
    public class ExtractedData
    {
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public string CostCentre { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
