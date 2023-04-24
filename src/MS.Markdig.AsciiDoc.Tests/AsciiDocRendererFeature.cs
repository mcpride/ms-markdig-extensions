using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xbehave;

namespace Markdig.Tests
{
    public class AsciiDocRendererFeature
    {
        private readonly ITestOutputHelper _output;
        private readonly MarkdownPipeline _pipeline;

        public AsciiDocRendererFeature(ITestOutputHelper output)
        {
            _output = output;
            _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseEmphasisExtras().Build();
        }

        [Scenario(DisplayName = "Render markdown")]
        [Example("simple-table", "a simple table")]
        public void RenderMarkdown(string content, string description)
        {
            MarkdownDocument document = null;
            string actual = null;
            $"Given a markdown document with {description}"
                .x(() => 
                {
                    var markdown = typeof(AsciiDocRendererFeature).Assembly
                        .ReadStringFromResource($"Markdig.Tests.MarkdownResources.{content}.md")
                        .Replace("\r\n", "\n")
                        .Replace("\r", "\n");
                    document = MarkdownParser.Parse(markdown, _pipeline);
                });
            $"When it will be rendered to AsciiDoc"
                .x(() => 
                {
                    var renderer = new AsciiDocRenderer(new StringWriter());
                    renderer.Render(document);
                    renderer.Writer.Flush();
                    actual = renderer.Writer.ToString();
                });
            $"Then the rendered result should be equal to the expected AsciiDoc"
                .x(() => 
                {
                    var expected = typeof(AsciiDocRendererFeature).Assembly
                        .ReadStringFromResource($"Markdig.Tests.ExpectedAsciiDoc.{content}.adoc")
                        .Replace("\r\n", "\n")
                        .Replace("\r", "\n");
                    Assert.Equal(expected, actual);
                });
        }
    }
}