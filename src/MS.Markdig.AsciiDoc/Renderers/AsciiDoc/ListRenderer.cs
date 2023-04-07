using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc;

/// <summary>
/// An AsciiDoc renderer for a <see cref="ListBlock"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{ListBlock}" />
public class ListRenderer : AsciiDocObjectRenderer<ListBlock>
{
    protected override void Write(AsciiDocRenderer renderer, ListBlock listBlock)
    {
        renderer.EnsureLine();

        if (renderer.ListIndentCount < 1)
        {
            renderer.WriteLine();
        }

        foreach (var item in listBlock)
        {
            var listItem = (ListItemBlock)item;

            var indentChar = listBlock.IsOrdered ? '.' : '*';
            if (listBlock.IsOrdered)
            {
                renderer.Write(". ");
            }
            else
            {
                renderer.Write("* ");
            }

            renderer.ListIndentCount++;
            try
            {
                renderer.PushIndent($"{indentChar}");
                renderer.WriteChildren(listItem);
            }
            finally
            {
                renderer.ListIndentCount--;
                renderer.PopIndent();
            }
        }
        renderer.EnsureLine();
    }
}