using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="DelimiterInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{DelimiterInline}" />
public class DelimiterInlineRenderer : AsciiDocObjectRenderer<DelimiterInline>
{
    protected override void Write(AsciiDocRenderer renderer, DelimiterInline obj)
    {
        renderer.Write(obj.ToLiteral());
        renderer.WriteChildren(obj);
    }
}