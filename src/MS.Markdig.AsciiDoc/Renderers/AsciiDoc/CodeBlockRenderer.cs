using Markdig.Helpers;
using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="CodeBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{CodeBlock}" />
    public class CodeBlockRenderer : AsciiDocObjectRenderer<CodeBlock>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            renderer.EnsureLine();
            renderer.WriteLine();
            renderer.Write("--");
            renderer.EnsureLine();

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
            }
            renderer.Write("--");
            renderer.EnsureLine();
        }
    }
}
