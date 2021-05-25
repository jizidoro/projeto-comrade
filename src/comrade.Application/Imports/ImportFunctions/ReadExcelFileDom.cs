#region

using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

#endregion

namespace comrade.Application.Imports.ImportFunctions
{
    public static class ReadExcelFileDom
    {
        public static void Execute(string fileName)
        {
            using var spreadsheetDocument = SpreadsheetDocument.Open(fileName, false);
            var workbookPart = spreadsheetDocument.WorkbookPart;
            if (workbookPart != null)
            {
                var worksheetPart = workbookPart.WorksheetParts.First();
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                string text;
                foreach (var r in sheetData.Elements<Row>())
                {
                    foreach (var c in r.Elements<Cell>())
                    {
                        text = c.CellValue.Text;
                        Console.Write(text + " ");
                    }
                }
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}