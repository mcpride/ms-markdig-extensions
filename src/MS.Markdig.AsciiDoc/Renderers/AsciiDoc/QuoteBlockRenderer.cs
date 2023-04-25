using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="QuoteBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{QuoteBlock}" />
    public class QuoteBlockRenderer : AsciiDocObjectRenderer<QuoteBlock>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, QuoteBlock obj)
        {
            if (!renderer.IsFirstInContainer)
            {
                renderer.EnsureLine();
            }

            if (renderer.QuoteIndentCount < 1)
            {
                renderer.WriteLine();
            }

            renderer.QuoteIndentCount++;
            try
            {
                renderer.PushIndent($"{obj.QuoteChar} ");
                renderer.WriteChildren(obj);
            }
            finally
            {
                renderer.QuoteIndentCount--;
                renderer.PopIndent();
            }

            if (!renderer.IsLastInContainer)
            {
                renderer.EnsureLine();
            }
        }
    }
}