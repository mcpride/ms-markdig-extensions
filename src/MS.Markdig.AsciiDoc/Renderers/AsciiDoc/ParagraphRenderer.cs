using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="ParagraphBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{ParagraphBlock}" />
    public class ParagraphRenderer : AsciiDocObjectRenderer<ParagraphBlock>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, ParagraphBlock obj)
        {

            if (!renderer.IsFirstInContainer)
            {
                renderer.EnsureLine();
                renderer.WriteLine();
            }

            renderer.WriteLeafInline(obj);
            renderer.EnsureLine();
        }
    }
}
