namespace ExtractText.Models
{
    public class ExtractedResult
    {
        public ExtractedData ExtractedData { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
