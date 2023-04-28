using Markdig.Syntax;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// A base class for AsciiDoc rendering <see cref="Block"/> and <see cref="Syntax.Inlines.Inline"/> Markdown objects.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="IMarkdownObjectRenderer" />
    public abstract class AsciiDocObjectRenderer<TObject> : MarkdownObjectRenderer<AsciiDocRenderer, TObject> where TObject : MarkdownObject
    {
    }
}
