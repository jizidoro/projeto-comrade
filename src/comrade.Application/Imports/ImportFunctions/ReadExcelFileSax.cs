#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;

#endregion

namespace comrade.Application.Imports.ImportFunctions
{
    public static class ReadExcelFileSax
    {
        public static List<Dictionary<string, string>> Execute(IFormFile arqivoImportacao)
        {
            try
            {
                using var streamArquivo = arqivoImportacao.OpenReadStream();
                using var spreadsheetDocument = SpreadsheetDocument.Open(streamArquivo, false);
                var workbookPart = spreadsheetDocument.WorkbookPart;

                var informacaoLinhas = new List<Dictionary<string, string>>();

                foreach (var worksheetPart in workbookPart.WorksheetParts)
                {
                    var informacaoLinha = new Dictionary<string, string>();
                    var reader = OpenXmlReader.Create(worksheetPart);
                    while (reader.Read())
                    {
                        if (reader.ElementType == typeof(Row))
                        {
                            reader.ReadFirstChild();

                            do
                            {
                                if (reader.ElementType == typeof(Cell))
                                {
                                    var celula = (Cell) reader.LoadCurrentElement();

                                    string cellValue;

                                    if (celula.DataType != null && celula.DataType == CellValues.SharedString)
                                    {
                                        var ssi = workbookPart.SharedStringTablePart.SharedStringTable
                                            .Elements<SharedStringItem>()
                                            .ElementAt(int.Parse(celula.CellValue.InnerText));

                                        cellValue = ssi.Text?.Text;
                                    }
                                    else
                                    {
                                        cellValue = celula.CellValue?.InnerText;
                                    }

                                    var coluna = Regex.Replace(celula.CellReference, @"[\d-]", string.Empty);
                                    informacaoLinha.Add(coluna, cellValue);
                                }
                            } while (reader.ReadNextSibling());
                        }

                        if (informacaoLinha.Count > 0)
                        {
                            informacaoLinhas.Add(informacaoLinha);
                            informacaoLinha = new Dictionary<string, string>();
                        }
                    }
                }

                streamArquivo.Close();
                return informacaoLinhas;
            }
            catch (Exception)
            {
                return new List<Dictionary<string, string>>();
            }
        }
    }
}