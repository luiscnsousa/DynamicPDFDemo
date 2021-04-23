using System.IO;
using DynamicPDFSample.Models;

namespace DynamicPDFSample.Services
{
    public interface IPdfService
    {
        ImageStream Rasterize(Stream pdfFile, int page, ImageOptions options);
    }
}
