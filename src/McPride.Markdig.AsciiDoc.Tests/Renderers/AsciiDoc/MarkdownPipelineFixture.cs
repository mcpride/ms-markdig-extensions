namespace Markdig.Renderers.AsciiDoc
{
    public class MarkdownPipelineFixture
    {
        public MarkdownPipeline Pipeline { get; }

        public MarkdownPipelineFixture()
        {
            Pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseEmphasisExtras()
                .Build();
        }
    }
}

