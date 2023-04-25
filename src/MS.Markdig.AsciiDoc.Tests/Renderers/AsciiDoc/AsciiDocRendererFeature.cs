using Markdig.Parsers;
using Markdig.Syntax;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Xbehave;

namespace Markdig.Renderers.AsciiDoc
{
    public class AsciiDocRendererFeature : IClassFixture<MarkdownPipelineFixture>
    {
        private readonly MarkdownPipelineFixture _fixture;
        private readonly ITestOutputHelper _output;

        public AsciiDocRendererFeature(
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
        public void RenderMarkdown(string description, string content)
        {
            MarkdownDocument document = null;
            string result = null;
            var a = typeof(AsciiDocRendererFeature).Assembly;
            var n = typeof(AsciiDocRendererFeature).Namespace;

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