using Markdig.Helpers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc;

/// <summary>
/// An AsciiDoc renderer for a <see cref="HtmlBlock"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{TObject}" />
public class HtmlBlockRenderer : AsciiDocObjectRenderer<HtmlBlock>
{
    protected override void Write(AsciiDocRenderer renderer, HtmlBlock obj)
    {
        if (renderer == null) throw new ArgumentNullException(nameof(renderer));
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var slices = obj.Lines.Lines;
        if (slices is not null)
        {
            renderer.EnsureLine();
            for (int i = 0; i < slices.Length; i++)
            {
                ref StringSlice slice = ref slices[i].Slice;
                if (slice.Text is null)
                {
                    break;
                }

                var span = slice.AsSpan();
                renderer.Write(span);
                renderer.EnsureLine();
            }
            renderer.WriteLine();
        }
    }
}