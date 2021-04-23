using DynamicPDFSample.Models;
using DynamicPDFSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DynamicPDFSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController
    {
        private readonly ILogger<PdfController> _logger;
        private readonly IPdfService _pdfService;

        public PdfController(
            ILogger<PdfController> logger,
            IPdfService pdfService)
        {
            _logger = logger;
            _pdfService = pdfService;
        }

        [HttpGet]
        [Route("{page}")]
        public IActionResult Image(int page)
        {
            var pdfStream = ResourceProvider.GetResourceStream("document.pdf");

            var options = new ImageOptions();
            var result = _pdfService.Rasterize(pdfStream, page, options);

            _logger.LogInformation($"Image generated with orientation: {result.Orientation}, height: {result.Height}, width: {result.Width}");

            return new FileStreamResult(result.InnerStream, "image/jpeg");
        }
    }
}
