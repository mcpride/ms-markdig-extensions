using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.AsciiDoc.Inlines;

/// <summary>
/// An AsciiDoc renderer for a <see cref="LinkInline"/>.
/// </summary>
/// <seealso cref="AsciiDocObjectRenderer{LinkInline}" />
public class LinkInlineRenderer : AsciiDocObjectRenderer<LinkInline>
{
    protected override void Write(AsciiDocRenderer renderer, LinkInline link)
    {
        var linkData = ReadId(ReadTitle(ReadUrl(ReadMacro(new AsciiDocLink(link)))));
        linkData.Text = renderer.RenderLinkText(link);

        if (renderer.AsciiDocLinkRewriter != null)
        {
            linkData = renderer.AsciiDocLinkRewriter(linkData);
        }

        if (!string.IsNullOrEmpty(linkData.Macro)) renderer.Write(linkData.Macro);
        if (!string.IsNullOrEmpty(linkData.Url)) renderer.Write(linkData.Url);
        renderer.Write("[");
        try
        {
            var quoteText = 
                (linkData.Text != null && linkData.Text.Contains(',')) 
                || !string.IsNullOrEmpty(linkData.Title)
                || !string.IsNullOrEmpty(linkData.Id);
            if (quoteText) renderer.Write('"');
            renderer.Write(linkData.Text);
            if (quoteText) renderer.Write('"');
            if (!string.IsNullOrEmpty(linkData.Title))
            {
                renderer.Write(", title=");
                renderer.Write('"');
                renderer.Write(linkData.Title);
                renderer.Write('"');
            }
            if (!string.IsNullOrEmpty(linkData.Id))
            {
                renderer.Write(", id=");
                renderer.Write(linkData.Id);
            }
        }
        finally
        {
            renderer.Write("]");
        }
    }

    private static AsciiDocLink ReadMacro(AsciiDocLink link)
    {
        link.Macro = link.Inline.IsImage ? "image::" : "link:";
        return link;
    }

    private static AsciiDocLink ReadUrl(AsciiDocLink link)
    {
        if (link.Inline.Url != null)
        {
            link.Url = Uri.TryCreate(link.Inline.Url, UriKind.RelativeOrAbsolute, out var url) ? url.ToString() : link.Inline.Url;
        }
        return link;
    }

    private static AsciiDocLink ReadTitle(AsciiDocLink link)
    {
        if (!string.IsNullOrEmpty(link.Inline.Title))
        {
            link.Title = link.Inline.Title;
        }
        return link;
    }

    private static AsciiDocLink ReadId(AsciiDocLink link)
    {
        if (link.Inline.Label != null)
        {
            if (link.Inline.LocalLabel == LocalLabel.Local || link.Inline.LocalLabel == LocalLabel.Empty)
            {
                if (link.Inline.LocalLabel == LocalLabel.Local)
                {
                    link.Id = link.Inline.LabelWithTrivia.Text;
                }
            }
        }
        return link;
    }
}