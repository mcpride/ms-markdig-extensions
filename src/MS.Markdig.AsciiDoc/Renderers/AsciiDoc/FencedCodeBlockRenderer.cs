using System.Globalization;
using System.Text;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="FencedCodeBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{FencedCodeBlock}" />
    public class FencedCodeBlockRenderer : AsciiDocObjectRenderer<FencedCodeBlock>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, FencedCodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            renderer.EnsureLine();
            renderer.WriteLine();

            var attributes = obj.TryGetAttributes();

            //var target = Slugify(attributes?.Id);

            var caption = attributes?.Properties?.FirstOrDefault(p =>
                p.Key.Equals("caption", StringComparison.OrdinalIgnoreCase)).Value;

            var target = Slugify(caption);
            target ??= HashContent(obj);

            if (!string.IsNullOrEmpty(obj.Info))
            {
                if (obj.Info.Equals("plantuml", StringComparison.OrdinalIgnoreCase))
                {

                    if (!string.IsNullOrEmpty(caption))
                    {
                        renderer.Write($".{caption}");
                        renderer.EnsureLine();
                    }

                    renderer.Write(target != null
                        ? $"[{obj.Info},target=\"{{plantumldir}}puml-{target}\",format=svg]"
                        : $"[{obj.Info},format=svg]");
                    renderer.EnsureLine();
                }
                else if (obj.Info.Equals("mermaid", StringComparison.OrdinalIgnoreCase))
                {
                    renderer.Write($"[{obj.Info}]");
                    renderer.EnsureLine();
                }
                else
                {
                    renderer.Write($"[source,{obj.Info}]");
                    renderer.EnsureLine();
                }
            }
            renderer.Write("----");
            renderer.EnsureLine();

            var slices = obj.Lines.Lines;
            if (slices is not null)
            {
                renderer.EnsureLine();
                for (var i = 0; i < slices.Length; i++)
                {
                    ref var slice = ref slices[i].Slice;
                    if (slice.Text is null)
                    {
                        break;
                    }

                    var span = slice.AsSpan();
                    renderer.Write(span);
                    renderer.EnsureLine();
                }
            }
            renderer.Write("----");
            renderer.EnsureLine();
        }

        private static string? HashContent(LeafBlock obj)
        {
            var slices = obj.Lines.Lines;
            var stream = new MemoryStream();
            for (var i = 0; i < slices.Length; i++)
            {
                ref var slice = ref slices[i].Slice;
                if (slice.Text is null)
                {
                    break;
                }
                stream.Write(Encoding.UTF8.GetBytes(slice.Text));
            }
            var hash = SHA256.Create();
            stream.Seek(0, SeekOrigin.Begin);
            var hashBytes = hash.ComputeHash(stream.ToArray());
            var sb = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private static string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            char[] chars = text
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                            != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        private static string? Slugify(string? phrase)
        {
            if (phrase == null) return null;
            var output = RemoveAccents(phrase).ToLower();
            output = Regex.Replace(output, @"[^A-Za-z0-9\s-]", "");
            output = Regex.Replace(output, @"\s+", " ").Trim();
            output = Regex.Replace(output, @"\s", "-");
            return output;
        }
    }
}
