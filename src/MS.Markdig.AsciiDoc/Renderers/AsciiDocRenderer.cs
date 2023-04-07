using System.Text.RegularExpressions;

namespace Markdig.Renderers
{
    public class AsciiDocRenderer : TextRendererBase<AsciiDocRenderer>
    {
        public string? DocumentTitle { get; set; }
        public string IdPrefix { get; set; } = "_";
        public string IdSeparator { get; set; } = "_";
        internal int ListIndentCount { get; set; }
        internal int QuoteIndentCount { get; set; }

        /// <summary>
        /// Allows links to be rewritten
        /// </summary>
        public Func<string, string>? LinkRewriter { get; set; }

        /// <summary>
        /// Allows AsciiDoc links to be rewritten
        /// </summary>
        public Func<AsciiDoc.AsciiDocLink, AsciiDoc.AsciiDocLink>? AsciiDocLinkRewriter { get; set; }

        /// <summary>
        /// Allows AsciiDoc headers to be rewritten
        /// </summary>
        public Func<string?, int, string>? HeaderRewriter { get; set; }


        /// <inheritdoc />
        public AsciiDocRenderer(TextWriter writer) : base(writer)
        {
            // Default block renderers
            ObjectRenderers.Add(new AsciiDoc.FencedCodeBlockRenderer());
            ObjectRenderers.Add(new AsciiDoc.CodeBlockRenderer());
            ObjectRenderers.Add(new AsciiDoc.ListRenderer());
            ObjectRenderers.Add(new AsciiDoc.HeadingRenderer());
            ObjectRenderers.Add(new AsciiDoc.HtmlBlockRenderer());
            ObjectRenderers.Add(new AsciiDoc.ParagraphRenderer());
            ObjectRenderers.Add(new AsciiDoc.QuoteBlockRenderer());
            ObjectRenderers.Add(new AsciiDoc.ThematicBreakRenderer());
            ObjectRenderers.Add(new AsciiDoc.TableRenderer());

            // Default inline renderers
            ObjectRenderers.Add(new AsciiDoc.Inlines.AutolinkInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.CodeInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.DelimiterInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.EmphasisInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.LineBreakInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.HtmlInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.HtmlEntityInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.LinkInlineRenderer());
            ObjectRenderers.Add(new AsciiDoc.Inlines.LiteralInlineRenderer());
        }

        public bool IsUriRelative(string uri)
        {
            return IsUriRelative(uri, out _);
        }
        public bool IsUriRelative(string uri, out Uri? relUri)
        {
            return Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out relUri) && !relUri.IsAbsoluteUri;
        }

        public string? Slugify(string? header)
        {
            return Slugify(header, IdPrefix, IdSeparator);
        }

        public string? Slugify(string? header, string idPrefix, string idSeparator)
        {
            if (string.IsNullOrEmpty(header)) return header;
            var slug = header.ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9\s]", " ");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = idPrefix + slug.Replace(" ", idSeparator);
            return slug;
        }

        public bool TryParseUrl(
            string? url, 
            out string? scheme,
            out string? authority,
            out string? path,
            out string? query,
            out string? fragment
            )
        {
            scheme = null;
            authority = null;
            path = null;
            query = null;
            fragment = null;
            if (string.IsNullOrEmpty(url)) return false;
            try
            {
                var r = new Regex(
                    @"^(?<s1>(?<s0>[^:/\?#]+):)?(?<a1>//(?<a0>[^/\?#]*))?(?<p0>[^\?#]*)(?<q1>\?(?<q0>[^#]*))?(?<f1>#(?<f0>.*))?", 
                    RegexOptions.ExplicitCapture);
                var m = r.Match(url);
                if (!m.Success) return false;
                if (m.Groups.ContainsKey("s0")) scheme = m.Groups["s1"].Value;
                if (m.Groups.ContainsKey("a0")) authority = m.Groups["a0"].Value;
                if (m.Groups.ContainsKey("p0")) path = m.Groups["p0"].Value;
                if (m.Groups.ContainsKey("q0")) query = m.Groups["q0"].Value;
                if (m.Groups.ContainsKey("f0")) fragment = m.Groups["f0"].Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string? RenderLinkText(Syntax.Inlines.LinkInline link)
        {
            using (var writer = new StringWriter())
            {
                var renderer = new AsciiDocRenderer(writer);
                renderer.WriteChildren(link);
                writer.Flush();
                return writer.ToString();
            }
        }

        public string? RenderHeader(Syntax.HeadingBlock header, int level)
        {
            using (var writer = new StringWriter())
            {
                var renderer = new AsciiDocRenderer(writer);
                var headingText = new string('=', level);
                renderer.Write($"{headingText} ");
                renderer.WriteLeafInline(header);
                writer.Flush();
                var text = writer.ToString();
                return text;
            }
        }
    }
}