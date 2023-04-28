using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="HtmlObjectRenderer{LiteralInline}" />
    public class LiteralInlineRenderer : AsciiDocObjectRenderer<LiteralInline>
    {
        protected override void Write(AsciiDocRenderer renderer, LiteralInline obj)
        {
            renderer.Write(ref obj.Content);
        }
    }
}
