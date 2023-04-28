using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="HeadingBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{HeadingBlock}" />
    public class HeadingRenderer : AsciiDocObjectRenderer<HeadingBlock>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, HeadingBlock obj)
        {
            renderer.EnsureLine();

            if (!renderer.IsFirstInContainer)
            {
                renderer.WriteLine();
            }

            var level = string.IsNullOrEmpty(renderer.DocumentTitle) ? obj.Level : obj.Level + 1;

            var header = renderer.RenderHeader(obj, level);
            if (renderer.HeaderRewriter != null)
            {
                header = renderer.HeaderRewriter(header, level);
            }
            if (header != null)
            {
                renderer.Write(header);
            }

            if (!renderer.IsLastInContainer)
            {
                renderer.EnsureLine();
            }
        }
    }
}
