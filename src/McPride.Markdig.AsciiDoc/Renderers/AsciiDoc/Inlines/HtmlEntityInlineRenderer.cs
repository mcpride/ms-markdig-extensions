using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="HtmlEntityInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{HtmlEntityInline}" />
public class HtmlEntityInlineRenderer : AsciiDocObjectRenderer<HtmlEntityInline>
{
    protected override void Write(AsciiDocRenderer renderer, HtmlEntityInline obj)
    {
        renderer.Write(obj.Transcoded);
    }
}