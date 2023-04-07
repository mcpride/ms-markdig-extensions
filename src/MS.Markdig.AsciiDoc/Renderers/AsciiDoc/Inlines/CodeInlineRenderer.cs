using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="CodeInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{CodeInline}" />
public class CodeInlineRenderer : AsciiDocObjectRenderer<CodeInline>
{
    protected override void Write(AsciiDocRenderer renderer, CodeInline obj)
    {
        renderer.Write($"`{obj.ContentSpan}`");
    }
}