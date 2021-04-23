using System.IO;
using System.Reflection;

namespace DynamicPDFSample.Services
{
    public static class ResourceProvider
    {
        public static Stream GetResourceStream(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedResourcePrefix = $"{assembly.GetName().Name}.Resources";

            return assembly.GetManifestResourceStream($"{embeddedResourcePrefix}.{filename}");
        }
    }
}
