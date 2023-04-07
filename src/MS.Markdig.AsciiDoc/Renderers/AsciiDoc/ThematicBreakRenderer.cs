using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc;

/// <summary>
/// An AsciiDoc renderer for a <see cref="ThematicBreakBlock"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{ThematicBreakBlock}" />
public class ThematicBreakRenderer : AsciiDocObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(AsciiDocRenderer renderer, ThematicBreakBlock obj)
    {
        renderer.EnsureLine();
        renderer.WriteLine();
        renderer.WriteLine("'''");
    }
}