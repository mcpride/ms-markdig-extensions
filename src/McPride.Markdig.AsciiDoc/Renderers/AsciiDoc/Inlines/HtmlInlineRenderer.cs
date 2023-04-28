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
            case "<br/>":
            case "<br />":
                renderer.WriteLine(obj.Column == 0 ? "{empty} +" : " +");
                break;
            case "<mark>":
                renderer.Write("##");
                break;
            case "</mark>":
                renderer.Write("##");
                break;
            case "<sup>" : 
                renderer.Write("^+");
                break;
            case "</sup>":
                renderer.Write("+^");
                break;
            case "<sub>":
                renderer.Write("~+");
                break;
            case "</sub>":
                renderer.Write("+~");
                break;
            case "<u>":
                renderer.Write("[.underline]#");
                break;
            case "</u>":
                renderer.Write('#');
                break;
            case "<b>":
                renderer.Write('*');
                break;
            case "</b>":
                renderer.Write('*');
                break;
            case "<strong>":
                renderer.Write('*');
                break;
            case "</strong>":
                renderer.Write('*');
                break;
            case "<del>":
                renderer.Write("[.line-through]#");
                break;
            case "</del>":
                renderer.Write('#');
                break;
            default:
                renderer.Write(obj.Tag);
                break;
        }
    }
}