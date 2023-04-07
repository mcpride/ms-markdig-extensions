using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;

var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions()/*.EnableTrackTrivia()*/.Build();
var document = MarkdownParser.Parse(File.ReadAllText("../../../github-flavored-markdown.md"), pipeline);
var renderer = new AsciiDocRenderer(new StringWriter());
renderer.Render(document);
renderer.Writer.Flush();
var result = renderer.Writer.ToString();
File.WriteAllText("../../../github-flavored-markdown.adoc", result);


