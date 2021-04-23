using System;
using System.IO;
using System.Linq;
using ceTe.DynamicPDF.Rasterizer;
using DynamicPDFSample.Models;
using Microsoft.Extensions.Logging;

namespace DynamicPDFSample.Services
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public ImageStream Rasterize(Stream pdfFile, int page, ImageOptions options)
        {
            pdfFile.Position = 0;

            Stream resultingFile;
            ImageFormat imageFormat;

            using var inputPdf = new InputPdf(pdfFile);
            using var rasterizer = new PdfRasterizer(inputPdf, page + 1, 1);

            switch (options.Format)
            {
                case ImageFormats.JPEG:
                    imageFormat = new JpegImageFormat(options.Quality);
                    break;

                case ImageFormats.PNG:
                    imageFormat = new PngImageFormat(PngColorFormat.RgbA);
                    break;

                default:
                    throw new NotSupportedException($"The provided format: {options.Format} is not supported");
            }

            var layout = GetImageLayout(inputPdf, page, options);

            try
            {
                resultingFile = new MemoryStream(
                    rasterizer.Draw(
                        imageFormat,
                        new FixedImageSize((float)layout.Width, (float)layout.Height))
                    .First());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error drawing image with PdfRasterizer");
                throw;
            }

            return new ImageStream(resultingFile, layout.Orientation, (int)layout.Width, (int)layout.Height);
        }

        private static (double Width, double Height, Orientation Orientation) GetImageLayout(InputPdf inputPdf, int page, ImageOptions options)
        {
            var pageHeight = inputPdf.Pages[page].Height;
            var pageWidth = inputPdf.Pages[page].Width;
            var lowerLimit = Math.Min(options.MaxHeight, options.MaxWidth);
            var orientation = pageHeight > pageWidth
                ? Orientation.Portrait
                : Orientation.Landscape;

            double height, width;
            if (orientation == Orientation.Portrait)
            {
                width = lowerLimit;
                height = pageHeight * width / pageWidth;
            }
            else
            {
                height = lowerLimit;
                width = pageWidth * height / pageHeight;
            }

            return (width, height, orientation);
        }
    }
}
