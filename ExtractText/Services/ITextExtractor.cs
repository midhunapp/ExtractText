using ExtractText.Models;

namespace ExtractText.Services
{
    public interface ITextExtractor
    {
        ExtractedResult ExtractText(string text);
    }
}
