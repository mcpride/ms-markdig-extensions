using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for an <see cref="EmphasisInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{EmphasisInline}" />
public class EmphasisInlineRenderer : AsciiDocObjectRenderer<EmphasisInline>
{
    protected override void Write(AsciiDocRenderer renderer, EmphasisInline obj)
    {
        var emTags = GetEmphasisTags(obj);
        renderer.Write(emTags.Item1);
        renderer.WriteChildren(obj);
        renderer.Write(emTags.Item2);
    }

    /// <summary>
    /// Gets the default emphasis tags for ** and __ emphasis.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    private static (string?,string?) GetEmphasisTags(EmphasisInline obj)
    {
        if (obj.DelimiterChar is '*' or '_')
        {
            return obj.DelimiterCount == 2 ?  ("_", "_") : ("*", "*");
        }
        return (null, null);
    }
}