#region

using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;

#endregion

namespace comrade.Application.Imports.ImportFunctions
{
    public static class ReadExcelFileDom
    {
        public static void Execute(IFormFile arqivoImportacao)
        {
            using var streamArquivo = arqivoImportacao.OpenReadStream();
            using var spreadsheetDocument = SpreadsheetDocument.Open(streamArquivo, false);
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
                        if (c.CellValue != null)
                        {
                            text = c.CellValue.Text;
                        }
                    }
                }
            }
        }
    }
}