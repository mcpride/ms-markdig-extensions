using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Syntax;

var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseEmphasisExtras()/*.EnableTrackTrivia()*/.Build();
var document = MarkdownParser.Parse(File.ReadAllText("../../../github-flavored-markdown.md"), pipeline);
PrintAST(document, 0);
var renderer = new AsciiDocRenderer(new StringWriter());
renderer.Render(document);
renderer.Writer.Flush();
var result = renderer.Writer.ToString();
File.WriteAllText("../../../github-flavored-markdown.adoc", result);


static void PrintAST(MarkdownObject? obj, int indentCount)
{
    if (obj == null) return;
    var indent = new string('.', indentCount);
    Console.Write(indent);
    Console.WriteLine(obj.GetType().Name);
    indentCount = indentCount + 2;
    foreach (var child in obj.Descendants())
    {
        PrintAST(child, indentCount);
    }
}

