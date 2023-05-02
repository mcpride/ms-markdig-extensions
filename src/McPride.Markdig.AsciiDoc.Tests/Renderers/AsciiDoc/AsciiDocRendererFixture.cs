using System.IO;

namespace Markdig.Renderers.AsciiDoc
{
    public class AsciiDocRendererFixture
    {
        public AsciiDocRenderer Renderer { get; }

        public AsciiDocRendererFixture()
        {
            Renderer = new AsciiDocRenderer(new StringWriter());
        }
    }
}

