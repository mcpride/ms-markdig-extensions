# The AsciiDoc renderer

With this renderer you can convert a [CommonMark](https://spec.commonmark.org/) compliant markdown document to an equivalent AsciiDoc document. The reason, why I wrote it, is, that my team mainly writes technical documentation in AsciiDoc format and converts it to multiple target formats like html, docbook, docx or pdf. Unfortunately there exist some pieces of documentation written in markdown e.g. architecture decision records, which are driven by markdown focused tools. With the help of the AsciiDoc renderer we can now include our ADRs into our arc42 based architecture documentation, written in AsciiDoc.

## Dependencies

* dotnet >= v6.0
* [Markdig nuget package](https://www.nuget.org/packages/Markdig)

## Status

The renderer is marked with a 0 as major version which means, this is in an early stage not ready for production! Use it at your own risk! Breaking changes are possible without increasing the major version. Updates and bugfixes will simply increase the minor version.

With update to version 1.0.0 and higher the versioning then will follow the semver pattern.

## Installation

You can add `McPride.Markdig.AsciiDoc` as nuget package to your project.

* dotnet cli: `dotnet add package McPride.Markdig.AsciiDoc`
* PS Package Manager: `Install-Package McPride.Markdig.AsciiDoc`
* nuget cli: `nuget install McPride.Markdig.AsciiDoc`

Alternativly you can also download the source code, include the `McPride.Markdig.AsciiDoc` project into your solution and reference it in your project. 

## Usage

See also the example project `gfm-to-adoc`!

```cs
var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
var document = MarkdownParser.Parse(File.ReadAllText("my-markdown-file.md"), pipeline);
var renderer = new AsciiDocRenderer(new StringWriter());
renderer.Render(document);
renderer.Writer.Flush();
File.WriteAllText("my-asciidoc-file.adoc", renderer.Writer.ToString());
```

## Examples

* gfm-to-adoc: Minimalistic example that converts the `src/examples/gfm-to-adoc/github-flavored-markdown.md` example markdown file to the `src/examples/gfm-to-adoc/github-flavored-markdown.adoc` AsciiDoc output file.

## License

The source code of this repository is under BSD 2-Clause license. See the [LICENSE](../../LICENSE) file for details.  
