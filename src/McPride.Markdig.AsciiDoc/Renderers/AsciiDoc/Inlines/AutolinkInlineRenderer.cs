using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for an <see cref="AutolinkInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{AutolinkInline}" />
public class AutolinkInlineRenderer : AsciiDocObjectRenderer<AutolinkInline>
{
    protected override void Write(AsciiDocRenderer renderer, AutolinkInline obj)
    {
        var url = obj.Url;
        if (renderer.LinkRewriter != null)
        {
            url = renderer.LinkRewriter(url);
        }
        renderer.Write(url);
    }
}