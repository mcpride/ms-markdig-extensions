using Markdig.Renderers.AsciiDoc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbehave;
using Xunit;
using Xunit.Abstractions;

namespace Markdig.Renderers
{
    public class AsciiDocRendererHelperFunctionsFeature : IClassFixture<AsciiDocRendererFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly AsciiDocRendererFixture _fixture;

        public AsciiDocRendererHelperFunctionsFeature(
            ITestOutputHelper output, 
            AsciiDocRendererFixture fixture)
        {
            _output = output;
            _fixture = fixture;
        }

        [Example("https://example.com/index.html", false)]
        [Example("../index.html", true)]
        [Scenario(DisplayName = "Check is uri relative")]
        public void CheckIsUriRelative(string uri, bool expected)
        {
            var actual = false;
            
            $"Given an uri {uri}".x(() => { });
            
            "When the uri will be checked that it is relative"
                .x(() =>
                {
                    actual = _fixture.Renderer.IsUriRelative(uri);
                });

            $"Then the result should be {expected}"
                .x(() =>
                {
                    Assert.Equal(expected, actual);
                });
        }

        [Example("Introduction to AsciiDoc - part 1", "_", "_", "_introduction_to_asciidoc_part_1")]
        [Example("Introduction to AsciiDoc - part 2", "_", "-", "_introduction-to-asciidoc-part-2")]
        [Example("Introduction to: AsciiDoc - (part 3)", "chapt01-", "-", "chapt01-introduction-to-asciidoc-part-3")]
        [Scenario(DisplayName = "Create a slug")]
        public void CreateSlug(string name, string idPrefix, string idSeparator, string expected)
        {
            var actual = string.Empty;

            $"Given the name {name} and the id prefix defined as {idPrefix} and the id separator defined as {idSeparator}"
                .x(() => { });

            "When a slug will be created with these parameters"
                .x(() =>
                {
                    _fixture.Renderer.IdPrefix = idPrefix;
                    _fixture.Renderer.IdSeparator = idSeparator;
                    actual = _fixture.Renderer.Slugify(name);
                });

            $"Then the expected result should be {expected}"
                .x(() =>
                {
                    Assert.Equal(expected, actual);
                });

        }

    }
    
}
