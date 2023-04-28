using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc;

public class AsciiDocLink
{
    public LinkInline Inline { get; }
    public string? Macro { get; set; }
    public string? Url { get; set; }
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Id { get; set; }

    public AsciiDocLink(LinkInline inline)
    {
        Inline = inline;
    }
}