using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="LineBreakInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{LineBreakInline}" />
public class LineBreakInlineRenderer : AsciiDocObjectRenderer<LineBreakInline>
{
    /// <summary>
    /// Gets or sets a value indicating whether to render this softline break as a hardline break tag
    /// </summary>
    public bool RenderAsHardlineBreak { get; set; }

    protected override void Write(AsciiDocRenderer renderer, LineBreakInline obj)
    {
        if (renderer.IsLastInContainer) return;

        if (obj.IsHard || RenderAsHardlineBreak)
        {
            renderer.WriteLine(obj.Column == 0 ? "{empty} +" : " +");
        }
        else
        {
            renderer.Write(' ');
        }

        //renderer.EnsureLine();
    }
}