namespace DynamicPDFSample.Models
{
    public class ImageOptions
    {
        private const int _maxWidth = 892;
        private const int _maxHeight = 1262;
        private const int _quality = 85;
        private const ImageFormats _format = ImageFormats.JPEG;

        public int MaxWidth { get; }
        public int MaxHeight { get; }
        public int Quality { get; }
        public ImageFormats Format { get; }

        public ImageOptions(int? maxWidth = null, int? maxHeight = null, int? quality = null, ImageFormats? format = null)
        {
            MaxWidth = maxWidth ?? _maxWidth;
            MaxHeight = maxHeight ?? _maxHeight;
            Quality = quality ?? _quality;
            Format = format ?? _format;
        }
    }
}
