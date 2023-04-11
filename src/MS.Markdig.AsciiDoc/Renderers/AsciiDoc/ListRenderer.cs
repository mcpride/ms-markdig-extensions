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

        var listIndentCount = ComputeIndentCount(listBlock);

        if (listIndentCount < 1)
        {
            renderer.WriteLine();
        }

        foreach (var item in listBlock)
        {
            var listItem = (ListItemBlock)item;

            var indentChar = listBlock.IsOrdered ? '.' : '*';
            renderer.Write($"{new string(indentChar, listIndentCount + 1)} ");
            renderer.WriteChildren(listItem);
        }
        renderer.EnsureLine();
    }

    private int ComputeIndentCount(ListBlock listBlock)
    {
        var result = 0;
        var parent = listBlock.Parent;
        while (parent != null)
        {
            if (parent is ListItemBlock)
            {
                result++;
            }
            else
            {
                if (parent is not ListBlock) break;
            }

            parent = parent.Parent;
        }
        return result;
    }
}