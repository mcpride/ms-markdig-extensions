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
            switch (obj.DelimiterCount)
            {
                case 2:
                  return ("*", "*");
                case 3:
                  return ("*_", "_*");
                default:
                  return ("_", "_");
            }
        }
        // strike through:
        else if ((obj.DelimiterChar is '~') && (obj.DelimiterCount == 2))
        {
            return ("[line-through]#", "#");
        }
        return (null, null);
    }
}