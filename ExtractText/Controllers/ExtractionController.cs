using ExtractText.Models;
using ExtractText.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExtractText.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractionController : ControllerBase
    {
        private readonly ITextExtractor _textExtractor;

        public ExtractionController(ITextExtractor textExtractor)
        {
            _textExtractor = textExtractor;
        }
        [HttpPost]
        public ActionResult<ExtractedData> ExtractData( string text)
        {
            var extractionResult = _textExtractor.ExtractText(text);

            if (extractionResult == null)
            {
                return BadRequest("Failed to extract data from the input text.");
            }
            else if (extractionResult.IsError)
            {
                return BadRequest(extractionResult.ErrorMessage);
            }

            return Ok(extractionResult.ExtractedData);
        }

    }
}
