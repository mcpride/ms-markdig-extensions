using Markdig.Extensions.Tables;
using System.Globalization;

namespace Markdig.Renderers.AsciiDoc
{
    /// <summary>
    /// An AsciiDoc renderer for a <see cref="HeadingBlock"/>.
    /// </summary>
    /// <seealso cref="AsciiDocObjectRenderer{HeadingBlock}" />
    public class TableRenderer : AsciiDocObjectRenderer<Table>
    {
        /// <inheritdoc />
        protected override void Write(AsciiDocRenderer renderer, Table table)
        {
            renderer.EnsureLine();
            renderer.WriteLine();

            var hasHeader = false;
            var columnCount = 0;
            for (var i = 0; i < table.Count; /*i++*/)
            {
                var rowObj = table[i];
                var row = (TableRow)rowObj;
                columnCount = row.Count;
                if (row.IsHeader)
                {
                    hasHeader = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            columnCount = Math.Min(columnCount, table.ColumnDefinitions.Count);

            var colDefs = new List<string>();
            for (var i = 0; i < columnCount; i++)
            {
                var columnDefinition = table.ColumnDefinitions[i];
                string colDef;
                if (columnDefinition.Width != 0.0f && columnDefinition.Width != 1.0f)
                {
                    var width = Math.Round(columnDefinition.Width * 100) / 100;
                    var widthValue = string.Format(CultureInfo.InvariantCulture, "{0:0.##}", width);
                    colDef = $"{widthValue}%";
                }
                else
                {
                    colDef = string.Empty;
                }

                if (columnDefinition.Alignment != null)
                {
                    switch (columnDefinition.Alignment)
                    {
                        case TableColumnAlign.Left :
                            colDef = $"<{colDef}";
                            break;
                        case TableColumnAlign.Center:
                            colDef = $"^{colDef}";
                            break;
                        case TableColumnAlign.Right:
                            colDef = $">{colDef}";
                            break;
                        // default:
                        //     colDef = $"<{colDef}";
                        //     break;
                    }
                }
                colDefs.Add(colDef);
            }

            renderer.Write("[cols=\"");
            var isFirst = true;
            foreach (var colDef in colDefs)
            {
                if (isFirst)
                {
                    renderer.Write(colDef);
                    isFirst = false;
                }
                else
                {
                    renderer.Write(',');
                    renderer.Write(colDef);
                }
            }
            renderer.Write("\"");
            if (hasHeader) renderer.Write(",options=\"header\"");
            renderer.Write(']');
            renderer.EnsureLine();
            renderer.Write("|===");
            renderer.EnsureLine();
            var isFirstRow = true;

            for (var i = 0; i < table.Count; i++)
            {
                var rowObj = table[i];
                var row = (TableRow)rowObj;
                if (!isFirstRow)
                {
                    renderer.WriteLine();
                }
                isFirstRow = false;
                for (var j = 0; j < row.Count; j++)
                {
                    renderer.Write('|');
                    var cellObj = row[j];
                    var cell = (TableCell)cellObj;
                    renderer.Write(cell);
                    renderer.EnsureLine();
                }
            }
            renderer.Write("|===");
            renderer.EnsureLine();
        }
    }
}
