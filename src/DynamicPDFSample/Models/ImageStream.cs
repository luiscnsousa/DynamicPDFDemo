using System.IO;

namespace DynamicPDFSample.Models
{
    public class ImageStream
    {
        public Stream InnerStream { get; }

        public Orientation Orientation { get; }

        public int? Width { get; }

        public int? Height { get; }

        public ImageStream(Stream stream, Orientation orientation, int width, int height)
        {
            InnerStream = stream;
            Orientation = orientation;
            Width = width;
            Height = height;
        }
    }
}
