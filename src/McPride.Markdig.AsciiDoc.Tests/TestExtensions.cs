using System.IO;
using System.Reflection;
using System.Text;

namespace Markdig
{
    public static class TestExtensions
    {
        public static string ReadStringFromResource(this Assembly assembly, string name)
        {
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                stream!.Position = 0;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd().SanitizeNewLine();
                }
            }
        }

        public static string SanitizeNewLine(this string text)
        {
            return text.Replace("\r\n", "\n").Replace("\r", "\n");
        }

    }
}