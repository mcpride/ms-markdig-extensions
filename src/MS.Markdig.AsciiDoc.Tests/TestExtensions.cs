using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Markdig.Tests
{
    public static class TestExtensions
    {
        public static string ReadStringFromResource(this Assembly assembly, string name)
        {
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                stream.Position = 0;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}