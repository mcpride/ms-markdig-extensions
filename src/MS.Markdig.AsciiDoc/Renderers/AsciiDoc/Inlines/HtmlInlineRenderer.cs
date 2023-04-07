using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="HtmlInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{HtmlInline}" />
public class HtmlInlineRenderer : AsciiDocObjectRenderer<HtmlInline>
{
    protected override void Write(AsciiDocRenderer renderer, HtmlInline obj)
    {
        switch (obj.Tag)
        {
            case "<br>":
                renderer.WriteLine("{empty} +");
                break;
            case "<mark>":
                renderer.Write('^');
                break;
            case "</mark>":
                renderer.Write('^');
                break;
            case "<sup>" : 
                renderer.Write('^');
                break;
            case "</sup>":
                renderer.Write('^');
                break;
            case "<sub>":
                renderer.Write('~');
                break;
            case "</sub>":
                renderer.Write('~');
                break;
            default:
                renderer.Write(obj.Tag);
                break;

        }
    }
}