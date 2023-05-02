using Markdig.Parsers;
using Markdig.Syntax;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Xbehave;

namespace Markdig.Renderers.AsciiDoc
{
    public class AsciiDocRenderingFeature : IClassFixture<MarkdownPipelineFixture>
    {
        private readonly MarkdownPipelineFixture _fixture;
        private readonly ITestOutputHelper _output;

        public AsciiDocRenderingFeature(
            ITestOutputHelper output, 
            MarkdownPipelineFixture fixture) 
        {
            _fixture = fixture;
            _output = output;
        }

        [Scenario(DisplayName = "Render Markdown to AsciiDoc")]
        [Example("a simple pipe table with headers", "tbl01")]
        [Example("a simple grid table with headers", "tbl02")]
        [Example("a simple grid table without headers", "tbl03")]
        [Example("a pipe table with column alignments", "tbl04")]
        [Example("different formatted text", "txt01")]
        [Example("a ruby code block", "code01")]
        [Example("a PlantUML diagram code block", "code02")]
        [Example("an unspecified code block", "code03")]
        [Example("an embedded code block", "code04")]
        [Example("an unordered list block", "lst01")]
        [Example("an ordered list block", "lst02")]
        [Example("headers", "hdr01")]
        [Example("links", "lnk01")]
        [Example("with a block quote", "qt01")]
        [Example("with a horizontal rule", "hr01")]
        [Example("with another horizontal rule", "hr02")]
        [Example("with a div html block", "hb01")]
        [Example("with a html entity inline", "he01")]
        [Example("with an autolink inline", "ali01")]
        [Example("with line breaks inline", "lbi01")]
        public void RenderMarkdown(string description, string content)
        {
            MarkdownDocument document = null;
            string result = null;
            var a = typeof(AsciiDocRenderingFeature).Assembly;
            var n = typeof(AsciiDocRenderingFeature).Namespace;

            $"Given a markdown document that contains {description}"
                .x(() =>
                {
                    var markdown = a.ReadStringFromResource($"{n}.MarkdownResources.{content}.md");
                    _output.WriteLine("Source Markdown:");
                    _output.WriteLine(markdown);
                    document = MarkdownParser.Parse(markdown, _fixture.Pipeline);
                });

            "When it will be rendered to AsciiDoc"
                .x(() =>
                {
                    var sut = new AsciiDocRenderer(new StringWriter());
                    sut.Render(document);
                    sut.Writer.Flush();
                    result = sut.Writer.ToString().SanitizeNewLine();
                    _output.WriteLine("Rendered AsciiDoc result:");
                    _output.WriteLine(result);
                });

            "Then the result is equal to the expected AsciiDoc"
                .x(() =>
                {
                    var expected = a.ReadStringFromResource($"{n}.ExpectedAsciiDoc.{content}.adoc");
                    _output.WriteLine("Expected AsciiDoc:");
                    _output.WriteLine(expected);
                    Assert.Equal(expected, result);
                });
        }
    }
}